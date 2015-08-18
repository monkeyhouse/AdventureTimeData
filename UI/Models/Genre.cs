using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class Tag : Metadata
    {
        public int ID { get; set; }

        [Required, MinLength(4), MaxLength(50)]
        public string Text { get; set; }

        public bool IsApproved { get; set; }        
    }
}