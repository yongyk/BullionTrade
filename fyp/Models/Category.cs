using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fyp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string Name{ get; set; }
    }
}
