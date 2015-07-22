using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Genre : Metadata
    {
        public int ID { get; set; }

        [Required, MinLength(4), MaxLength(50)]
        public string Text { get; set; }
        
    }
}