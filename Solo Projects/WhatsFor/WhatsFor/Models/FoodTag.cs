using System.ComponentModel.DataAnnotations;

namespace WhatsFor.Models
{
    public class FoodTag
    {
        [Required(ErrorMessage = "Tag names cannot be empty")]
        [Display(Name = "Tags")]
        [StringLength(50)]
        public string Name { get; set; }

        
    }
}