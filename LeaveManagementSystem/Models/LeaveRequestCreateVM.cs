using LeaveManagementSystem.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Models
{
    public class LeaveRequestCreateVM : IValidatableObject
    {
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public SelectList? LeaveType { get; set; }
        [Required]
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }
        [Required]
        [Display(Name = "Request Comments")]
        public string? RequestComments { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                yield return new ValidationResult("The Start date must be before end date", new[] { nameof(StartDate), nameof(EndDate) });
            }

            if(RequestComments?.Length > 250)
            {
                yield return new ValidationResult("Maximum characters for request comments cannot be more than 250", new[] { nameof(RequestComments) });

            }
        }
    }
}
