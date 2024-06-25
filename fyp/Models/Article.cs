using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace fyp.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Content { get; set; } 
        public string DateCreated { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        [ValidateNever] 
        public string ImageUrl { get; set; }    
    }
}
