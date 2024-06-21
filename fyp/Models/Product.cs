using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fyp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1,10000000)]
        public double Price { get; set; }

        public int CategoryId { get; set; }
        //foreign key
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        //linking to other table (Category table)
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
