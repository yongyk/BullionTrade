using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fyp.Models
{
    public class Selling
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Weight is required")]
        public string Weight { get; set; }
        [Required(ErrorMessage = "Product Purity is required")]
        [DisplayName("Product Purity")]
        public string ProductPurity { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]

        public string Email { get; set; }

        [DisplayName("Appointment Slot")]

        [ValidateNever]
        public int AppointmentSlotId { get; set; }
        [ForeignKey("AppointmentSlotId")]
        [ValidateNever]

        public AppointmentSlot ApplicationSlot { get; set; }
      
        [NotMapped]

        public int SelectedDateId { get; set; }  // Temporary property for the date dropdown
        [NotMapped]
        public int SelectedTimeId { get; set; }  // Temporary property for the time dropdown
       
    }
}
