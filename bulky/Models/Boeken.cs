using System.ComponentModel.DataAnnotations;
namespace bulky.Models
{
    public class Boeken
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Naam { get; set; }
        public int DisplayOrder { get; set; }

    }
}
