namespace ProductionPlan.Forms
{
    partial class FrmProductionPlan
    {
        private System.ComponentModel.IContainer components = null;

        // Main panel that hosts the GridControl
        private System.Windows.Forms.Panel  panelMain;
        private System.Windows.Forms.Panel  panelTop;
        private System.Windows.Forms.Label  lblTitle;
        private System.Windows.Forms.Label  lblDateRange;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop   = new System.Windows.Forms.Panel();
            this.lblTitle   = new System.Windows.Forms.Label();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport  = new System.Windows.Forms.Button();
            this.panelMain  = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.panelTop.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height    = 56;
            this.panelTop.Controls.AddRange(new System.Windows.Forms.Control[]
                { this.lblTitle, this.lblDateRange, this.btnRefresh, this.btnExport });

            // lblTitle
            this.lblTitle.AutoSize  = false;
            this.lblTitle.Text      = "Production Plan";
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location  = new System.Drawing.Point(12, 14);
            this.lblTitle.Size      = new System.Drawing.Size(220, 28);

            // lblDateRange
            this.lblDateRange.AutoSize  = false;
            this.lblDateRange.Text      = System.DateTime.Today.ToString("dd MMM yyyy");
            this.lblDateRange.ForeColor = System.Drawing.Color.FromArgb(189, 195, 199);
            this.lblDateRange.Font      = new System.Drawing.Font("Segoe UI", 10f);
            this.lblDateRange.Location  = new System.Drawing.Point(235, 18);
            this.lblDateRange.Size      = new System.Drawing.Size(180, 22);

            // btnExport
            this.btnExport.Text      = "Export";
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnExport.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnExport.Size      = new System.Drawing.Size(90, 30);
            this.btnExport.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnExport.Location  = new System.Drawing.Point(this.ClientSize.Width - 110, 13);
            this.btnExport.Click    += new System.EventHandler(this.btnExport_Click);

            // btnRefresh
            this.btnRefresh.Text      = "Refresh";
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.btnRefresh.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Size      = new System.Drawing.Size(90, 30);
            this.btnRefresh.Anchor    = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnRefresh.Location  = new System.Drawing.Point(this.ClientSize.Width - 210, 13);
            this.btnRefresh.Click    += new System.EventHandler(this.btnRefresh_Click);

            // panelMain
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;

            // FrmProductionPlan
            this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1200, 650);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name        = "FrmProductionPlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text        = "Production Plan";

            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ---- Button event stubs (implement as needed) ---- //

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            LoadSampleData();
        }

        private void btnExport_Click(object sender, System.EventArgs e)
        {
            using (var dlg = new System.Windows.Forms.SaveFileDialog
            {
                Filter   = "Excel Workbook (*.xlsx)|*.xlsx|CSV File (*.csv)|*.csv",
                FileName = "ProductionPlan_" + System.DateTime.Today.ToString("yyyyMMdd")
            })
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (dlg.FilterIndex == 1)
                        bandedView.ExportToXlsx(dlg.FileName);
                    else
                        bandedView.ExportToCsv(dlg.FileName);

                    System.Windows.Forms.MessageBox.Show("Exported successfully!", "Export",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
        }
    }
}
