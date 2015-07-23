using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data
{
    public abstract class Metadata
    {      
        [Required]
        public DateTime CreatedOn { get; set; }

        [Column("CreatedByID")]
        public string CreatedBy{ get; set; }
        [ForeignKey("CreatedBy")]
        public IdentityUser CreatedByUser { get; set; }   

        [Required]
        public DateTime ModifiedOn { get; set; }

        [Column("ModifiedByID")]
        public string ModifiedBy { get; set; }
        [ForeignKey("ModifiedBy")]
        public IdentityUser ModifiedByUser { get; set; }   

    }
}