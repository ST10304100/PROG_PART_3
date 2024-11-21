namespace PROG_PART_2.Models
{
    // ViewModel used for generating a new report in the system
    public class GenerateReportViewModel
    {
        // Name of the report to be generated
        public string ReportName { get; set; }

        // Start date for the report's data range
        public DateTime StartDate { get; set; }

        // End date for the report's data range
        public DateTime EndDate { get; set; }

        // Type of the report (e.g., PDF, Excel)
        public string ReportType { get; set; }

        // List of existing reports, to provide options for selecting or using previously generated reports
        public List<Report> ExistingReports { get; set; } = new List<Report>();
    }
}
