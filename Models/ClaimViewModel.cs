using System.ComponentModel.DataAnnotations;

namespace PROG_PART_2.Models
{
    // The ClaimViewModel class is used to capture and validate data for a claim submission.
    public class ClaimViewModel
    {
        // Property to store the number of hours worked for the claim
        // Validation: Required and must be between 1 and 150 hours
        [Required(ErrorMessage = "Hours Worked is required.")]
        [Range(1, 150, ErrorMessage = "Hours Worked must be between 1 and 150.")]
        public decimal HoursWorked { get; set; }

        // Property to store the hourly rate for the claim submission
        // Validation: Required and must be between 200 and 1000
        [Required(ErrorMessage = "Hourly Rate is required.")]
        [Range(200, 1000, ErrorMessage = "Hourly Rate must be between 200 and 1000.")]
        public decimal HourlyRate { get; set; }

        // Property for additional notes about the claim
        // Validation: Maximum length of 500 characters for notes
        [MaxLength(500, ErrorMessage = "Notes can't exceed 500 characters.")]
        public string Notes { get; set; }

        // Property to store supporting documents related to the claim submission
        // The property is of type List<IFormFile> to allow multiple file uploads
        [Display(Name = "Supporting Documents")]
        public List<IFormFile> SupportingDocuments { get; set; }

        // Property to store the start date of the claim
        // Validation: Required field
        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        // Property to store the end date of the claim
        // Validation: Required field
        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }
    }
}
