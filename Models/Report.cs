using System.ComponentModel.DataAnnotations;

namespace PROG_PART_2.Models
{
    // Represents a Report with various validation rules for its properties
    public class Report
    {
        // Unique identifier for the Report
        public int ReportId { get; set; }

        // The name of the report, which is required and cannot exceed 100 characters
        [Required(ErrorMessage = "Report Name is required.")]
        [StringLength(100, ErrorMessage = "Report Name cannot exceed 100 characters.")]
        public string ReportName { get; set; }

        // The type of the report, which is required and cannot exceed 50 characters
        [Required(ErrorMessage = "Report Type is required.")]
        [StringLength(50, ErrorMessage = "Report Type cannot exceed 50 characters.")]
        public string ReportType { get; set; }

        // The start date of the report, which is required
        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        // The end date of the report, which is required and must be after the Start Date
        [Required(ErrorMessage = "End Date is required.")]
        [DateGreaterThan("StartDate", ErrorMessage = "End Date must be after Start Date.")]
        public DateTime EndDate { get; set; }

        // The file path of the report, which is required and must match the file type restrictions
        [Required(ErrorMessage = "File Path is required.")]
        [StringLength(200, ErrorMessage = "File Path cannot exceed 200 characters.")]
        [RegularExpression(@"^.*\.(pdf|docx|xlsx)$", ErrorMessage = "File Path must be a .pdf, .docx, or .xlsx file.")]
        public string FilePath { get; set; }
    }
}
