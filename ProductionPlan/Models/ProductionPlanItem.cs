using System;

namespace ProductionPlan.Models
{
    /// <summary>
    /// Represents a single row in the production plan.
    /// </summary>
    public class ProductionPlanItem
    {
        // -- Identification band --
        public int    RowNo        { get; set; }
        public string Family       { get; set; }
        public string LineId       { get; set; }

        // -- Schedule band --
        public DateTime Date       { get; set; }
        public string   Shift      { get; set; }    // e.g. "Morning", "Afternoon", "Night"
        public string   TeamLeader { get; set; }

        // -- Product band --
        public string ProductNo    { get; set; }
        public string Description  { get; set; }
        public string UOM          { get; set; }    // Unit of Measure

        // -- Quantity band --
        public int    Target       { get; set; }
        public int    Actual       { get; set; }
        public int    Defect       { get; set; }

        // -- Performance band --
        public double Efficiency   => Target > 0 ? Math.Round((double)Actual / Target * 100, 1) : 0;
        public string Status       { get; set; }    // e.g. "On Track", "Delayed", "Complete"
        public string Remarks      { get; set; }
    }
}
