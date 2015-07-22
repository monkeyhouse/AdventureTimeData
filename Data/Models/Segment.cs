using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Segment : Metadata
    {
        public Segment()
        {
            Leaders = new HashSet<Action>();
            Actions = new HashSet<Action>();
        }
        public int ID { get; set; }

        [Required, MinLength(5), MaxLength(3000)]
        public string Text { get; set; }

        [Required]
        public bool IsEnding { get; set; }

        [InverseProperty("Child")] 
        public virtual ICollection<Action> Leaders { get; set; }

        [InverseProperty("Parent")] 
        public virtual ICollection<Action> Actions { get; set; }
    }
}