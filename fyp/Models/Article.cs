using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace fyp.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Content is required.")]

        public string Content { get; set; } 
        public string DateCreated { get; set; }
        [Required(ErrorMessage = "Author is required.")]

        public string Author { get; set; }
        [Required(ErrorMessage = "Title is required.")]

        public string Title { get; set; }
        [ValidateNever] 
        public string ImageUrl { get; set; }    
    }
}
