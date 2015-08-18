using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI.Models
{
    public class Story : Metadata
    {
        public int ID { get; set; }

        [MaxLength(150), Required]
        public string Title { get; set; }

        [MaxLength(500), Required]
        public string Summary { get; set; }

        [MaxLength(250)]
        public string Settings { get; set; }

        public bool IsLocked { get; set; }

        public virtual Page FirstPage { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        [InverseProperty("Story")]
        public virtual ICollection<Page>  Pages { get; set; }
        [InverseProperty("Story")]
        public virtual ICollection<Action> Actions { get; set; }
        
    }

    //public class StoryRating
    //{
    //    public int ID { get; set; }

    //    [Column("UserID")]
    //    public string UserID { get; set; }
    //    [ForeignKey("UserID")]
    //    public IdentityUser User { get; set; }

    //    public Story Story { get; set; }

    //    public StarRating Value { get; set; }
    //}

    //public enum StarRating
    //{
    //    Undefined = 0,
    //    OneStar = 1,
    //    TwoStar = 2,
    //    ThreeStar = 3,
    //    FourStar = 4,
    //    FiveStar = 5
    //}
}