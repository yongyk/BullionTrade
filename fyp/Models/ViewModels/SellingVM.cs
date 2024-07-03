using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace fyp.Models.ViewModels
{
    public class SellingVM
    {
        public Selling Selling { get; set; } =new Selling();    
        [ValidateNever]
        public IEnumerable<SelectListItem> AppointmentList { get; set; }
       // [ValidateNever]
       // public IEnumerable<SelectListItem> DateList { get; set; }   
     
            [ValidateNever]

        public IEnumerable<SelectListItem> MetalPurity { get; set; }
        [ValidateNever]
        [EmailAddress(ErrorMessage = "Invalid email address")]

        public string Email {  get; set; }  
    }
}
