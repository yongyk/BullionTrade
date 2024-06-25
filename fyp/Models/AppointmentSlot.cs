using System.ComponentModel.DataAnnotations;

namespace fyp.Models
{
    public class AppointmentSlot
    {
        public int Id { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Time { get; set; }
    }
}
