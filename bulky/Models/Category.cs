using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Categroy Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="Value moet tussen 1 en 200 zijn")]
        
        public int DisplayOrder { get; set; }
       
    }
}
