using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using ProductionPlan.Models;

namespace ProductionPlan.Forms
{
    public partial class FrmProductionPlan : Form
    {
        private GridControl         gridControl;
        private BandedGridView      bandedView;

        public FrmProductionPlan()
        {
            InitializeComponent();
            SetupGrid();
            LoadSampleData();
        }

        // ------------------------------------------------------------------ //
        //  Grid setup                                                          //
        // ------------------------------------------------------------------ //

        private void SetupGrid()
        {
            gridControl = new GridControl { Dock = DockStyle.Fill };
            bandedView  = new BandedGridView();

            gridControl.MainView = bandedView;
            gridControl.ViewCollection.Add(bandedView);

            ConfigureBandedView();
            CreateBandsAndColumns();
            ApplyAppearance();

            panelMain.Controls.Add(gridControl);
        }

        private void ConfigureBandedView()
        {
            bandedView.OptionsView.ShowGroupPanel      = false;
            bandedView.OptionsView.ShowIndicator       = true;
            bandedView.OptionsView.ColumnAutoWidth      = false;
            bandedView.OptionsView.EnableAppearanceOddRow  = true;
            bandedView.OptionsView.EnableAppearanceEvenRow = true;
            bandedView.OptionsSelection.MultiSelect    = true;
            bandedView.OptionsBehavior.Editable        = false;
            bandedView.OptionsBehavior.AutoExpandAllGroups = false;
        }

        private void CreateBandsAndColumns()
        {
            // ---- Band 1: Identification ---- //
            var bandId = AddBand("Identification", Color.FromArgb(41, 128, 185));
            AddColumn(bandId, "RowNo",        "#",           40,  false);
            AddColumn(bandId, "Family",       "Family",      90,  false);
            AddColumn(bandId, "LineId",       "Line ID",     75,  false);

            // ---- Band 2: Schedule ---- //
            var bandSched = AddBand("Schedule", Color.FromArgb(39, 174, 96));
            AddColumn(bandSched, "Date",        "Date",        95,  false, "dd/MM/yyyy");
            AddColumn(bandSched, "Shift",       "Shift",       80,  false);
            AddColumn(bandSched, "TeamLeader",  "Team Leader", 110, false);

            // ---- Band 3: Product ---- //
            var bandProd = AddBand("Product", Color.FromArgb(142, 68, 173));
            AddColumn(bandProd, "ProductNo",   "Product No",  100, false);
            AddColumn(bandProd, "Description", "Description", 160, false);
            AddColumn(bandProd, "UOM",         "UOM",         55,  false);

            // ---- Band 4: Quantity ---- //
            var bandQty = AddBand("Quantity", Color.FromArgb(230, 126, 34));
            AddColumn(bandQty, "Target",  "Target",  75, false);
            AddColumn(bandQty, "Actual",  "Actual",  75, false);
            AddColumn(bandQty, "Defect",  "Defect",  75, false);

            // ---- Band 5: Performance ---- //
            var bandPerf = AddBand("Performance", Color.FromArgb(192, 57, 43));
            AddColumn(bandPerf, "Efficiency", "Efficiency %", 90,  false);
            AddColumn(bandPerf, "Status",     "Status",       90,  false);
            AddColumn(bandPerf, "Remarks",    "Remarks",      160, false);

            // Custom cell appearance: colour Status column
            bandedView.CustomDrawCell += BandedView_CustomDrawCell;
        }

        // ------------------------------------------------------------------ //
        //  Helpers                                                             //
        // ------------------------------------------------------------------ //

        private GridBand AddBand(string caption, Color headerColor)
        {
            var band = new GridBand
            {
                Caption = caption,
                AppearanceHeader =
                {
                    BackColor   = headerColor,
                    ForeColor   = Color.White,
                    Font        = new Font("Segoe UI", 9f, FontStyle.Bold),
                    TextOptions = { HAlignment = DevExpress.Utils.HorzAlignment.Center }
                }
            };
            bandedView.Bands.Add(band);
            return band;
        }

        private BandedGridColumn AddColumn(GridBand band, string fieldName, string caption,
                                            int width, bool visible = true, string formatString = null)
        {
            var col = new BandedGridColumn
            {
                FieldName = fieldName,
                Caption   = caption,
                Width     = width,
                Visible   = visible,
                OptionsColumn = { AllowEdit = false }
            };

            if (formatString != null)
                col.DisplayFormat.FormatString = formatString;

            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            col.AppearanceCell.TextOptions.HAlignment   = DevExpress.Utils.HorzAlignment.Center;

            bandedView.Columns.Add(col);
            band.Columns.Add(col);
            col.Visible = true;

            return col;
        }

        private void ApplyAppearance()
        {
            // Alternating row colours
            bandedView.Appearance.OddRow.BackColor  = Color.White;
            bandedView.Appearance.EvenRow.BackColor = Color.FromArgb(240, 248, 255);

            // Header row
            bandedView.Appearance.HeaderPanel.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            bandedView.Appearance.HeaderPanel.BackColor = Color.FromArgb(52, 73, 94);
            bandedView.Appearance.HeaderPanel.ForeColor = Color.White;

            // Selected row
            bandedView.Appearance.SelectedRow.BackColor = Color.FromArgb(174, 214, 241);
            bandedView.Appearance.FocusedRow.BackColor  = Color.FromArgb(133, 193, 233);
        }

        // ------------------------------------------------------------------ //
        //  Custom cell drawing – colour Status values                         //
        // ------------------------------------------------------------------ //

        private void BandedView_CustomDrawCell(object sender,
            DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "Status") return;

            var status = e.CellValue?.ToString() ?? string.Empty;
            switch (status)
            {
                case "Complete":
                    e.Appearance.BackColor = Color.FromArgb(39, 174, 96);
                    e.Appearance.ForeColor = Color.White;
                    break;
                case "On Track":
                    e.Appearance.BackColor = Color.FromArgb(41, 128, 185);
                    e.Appearance.ForeColor = Color.White;
                    break;
                case "Delayed":
                    e.Appearance.BackColor = Color.FromArgb(192, 57, 43);
                    e.Appearance.ForeColor = Color.White;
                    break;
                case "Pending":
                    e.Appearance.BackColor = Color.FromArgb(243, 156, 18);
                    e.Appearance.ForeColor = Color.White;
                    break;
            }
        }

        // ------------------------------------------------------------------ //
        //  Sample data                                                         //
        // ------------------------------------------------------------------ //

        private void LoadSampleData()
        {
            var today = DateTime.Today;
            var data = new List<ProductionPlanItem>
            {
                new ProductionPlanItem { RowNo=1,  Family="Electronics", LineId="L-01", Date=today,      Shift="Morning",   TeamLeader="Alice Wong",   ProductNo="PRD-1001", Description="PCB Assembly A",     UOM="PCS", Target=500, Actual=492, Defect=3,  Status="On Track",  Remarks="" },
                new ProductionPlanItem { RowNo=2,  Family="Electronics", LineId="L-01", Date=today,      Shift="Afternoon", TeamLeader="Bob Tan",      ProductNo="PRD-1002", Description="PCB Assembly B",     UOM="PCS", Target=450, Actual=450, Defect=0,  Status="Complete",  Remarks="Ahead of schedule" },
                new ProductionPlanItem { RowNo=3,  Family="Electronics", LineId="L-02", Date=today,      Shift="Morning",   TeamLeader="Carol Lim",    ProductNo="PRD-1003", Description="Sensor Module X1",   UOM="PCS", Target=300, Actual=210, Defect=12, Status="Delayed",   Remarks="Material shortage" },
                new ProductionPlanItem { RowNo=4,  Family="Mechanical",  LineId="L-03", Date=today,      Shift="Night",     TeamLeader="David Ng",     ProductNo="PRD-2001", Description="Bracket Assembly",   UOM="SET", Target=200, Actual=198, Defect=2,  Status="On Track",  Remarks="" },
                new ProductionPlanItem { RowNo=5,  Family="Mechanical",  LineId="L-03", Date=today,      Shift="Morning",   TeamLeader="Eva Chen",     ProductNo="PRD-2002", Description="Frame Sub-Assy",     UOM="SET", Target=150, Actual=0,   Defect=0,  Status="Pending",   Remarks="Waiting setup" },
                new ProductionPlanItem { RowNo=6,  Family="Packaging",   LineId="L-04", Date=today,      Shift="Afternoon", TeamLeader="Frank Ho",     ProductNo="PRD-3001", Description="Carton Box Pack",    UOM="CTN", Target=800, Actual=820, Defect=5,  Status="Complete",  Remarks="" },
                new ProductionPlanItem { RowNo=7,  Family="Electronics", LineId="L-02", Date=today.AddDays(1), Shift="Morning", TeamLeader="Grace Yeo", ProductNo="PRD-1004", Description="Control Board Rev2", UOM="PCS", Target=400, Actual=0,   Defect=0,  Status="Pending",   Remarks="" },
                new ProductionPlanItem { RowNo=8,  Family="Mechanical",  LineId="L-05", Date=today.AddDays(1), Shift="Afternoon", TeamLeader="Henry Koh", ProductNo="PRD-2003", Description="Shaft Assy Type B", UOM="SET", Target=120, Actual=0,   Defect=0,  Status="Pending",   Remarks="" },
                new ProductionPlanItem { RowNo=9,  Family="Packaging",   LineId="L-04", Date=today.AddDays(1), Shift="Night",  TeamLeader="Irene Tay",  ProductNo="PRD-3002", Description="Blister Pack Std",  UOM="PCS", Target=600, Actual=0,   Defect=0,  Status="Pending",   Remarks="" },
                new ProductionPlanItem { RowNo=10, Family="Electronics", LineId="L-01", Date=today.AddDays(1), Shift="Night",  TeamLeader="Jack Sim",   ProductNo="PRD-1005", Description="Power Supply Unit", UOM="PCS", Target=250, Actual=0,   Defect=0,  Status="Pending",   Remarks="" },
            };

            gridControl.DataSource = data;
        }
    }
}
