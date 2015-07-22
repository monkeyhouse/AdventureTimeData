using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Action : Metadata
    {

        public int ID { get; set; }

        [Required, MinLength(4), MaxLength(500)]
        public string Text { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey("ParentId"), Required]
        public virtual Segment Parent { get; set; }

        public int? ChildId { get; set; }
        [ForeignKey("ChildId")]
        public virtual Segment Child { get; set; }
        
    }
}