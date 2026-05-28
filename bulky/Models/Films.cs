using System.ComponentModel.DataAnnotations;

namespace bulky.Models
{
    public class Films
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
