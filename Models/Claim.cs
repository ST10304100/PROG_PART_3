using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROG_PART_2.Models
{
    // The Claim class represents a claim submitted by a user in the system
    public class Claim
    {
        // Unique identifier for each claim
        public int ClaimId { get; set; }

        // Property to store the number of hours worked
        // Validation: Required and must be between 1 and 150
        [Required(ErrorMessage = "Hours Worked is required.")]
        [Range(1, 150, ErrorMessage = "Hours Worked must be between 1 and 150.")]
        public decimal HoursWorked { get; set; }

        // Property to store the hourly rate for the claim
        // Validation: Required and must be between 200 and 1000
        [Required(ErrorMessage = "Hourly Rate is required.")]
        [Range(200, 1000, ErrorMessage = "Hourly Rate must be between 200 and 1000.")]
        public decimal HourlyRate { get; set; }

        // Property to store the total amount for the claim
        // Validation: Required
        [Required]
        public decimal TotalAmount { get; set; }

        // Property for additional notes regarding the claim
        // Validation: Max length of 500 characters
        [MaxLength(500, ErrorMessage = "Notes can't exceed 500 characters.")]
        public string Notes { get; set; }

        // Property to store the date when the claim was submitted
        // Validation: Required
        [Required]
        public DateTime DateSubmitted { get; set; }

        // Property to store the current status of the claim (default value is "Pending")
        public string Status { get; set; } = "Pending";

        // Property to track whether the claim has been approved by the coordinator (default is false)
        public bool IsApprovedByCoordinator { get; set; } = false;

        // Property to track whether the claim has been approved by the manager (default is false)
        public bool IsApprovedByManager { get; set; } = false;

        // Foreign Key: Links the claim to an ApplicationUser (user who submitted the claim)
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        // Navigation property: Represents the ApplicationUser who submitted the claim
        public virtual ApplicationUser ApplicationUser { get; set; }

        // Navigation property: Represents the collection of documents associated with the claim
        public virtual ICollection<Document> Documents { get; set; }

        // Property to store the start date of the claim
        // Validation: Required
        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        // Property to store the end date of the claim
        // Validation: Required
        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }

        // Property to store the payment status of the claim (default value is "Pending")
        [Required]
        public string PaymentStatus { get; set; } = "Pending";
    }
}
