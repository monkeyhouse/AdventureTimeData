using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI.Models
{
    public class Action : Metadata
    {

        public int ID { get; set; }

        [Required, MinLength(4), MaxLength(500)]
        public string Text { get; set; }

        public int? ParentID { get; set; }
        [ForeignKey("ParentID"), Required]
        public virtual Page Parent { get; set; }

        public int? ChildID { get; set; }
        [ForeignKey("ChildID")]
        public virtual Page Child { get; set; }

        public virtual Story Story { get; set; }
    }
}