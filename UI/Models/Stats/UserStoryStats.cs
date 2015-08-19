using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI.Models
{
    /// <summary>
    /// Created to track user stats to provide fun feedback
    /// </summary>
    public class UserStoryStats
    {
        public int ID { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public IdentityUser User { get; set; }

        public Story Story { get; set; }

        public int Plays { get; set; }
        public int Pages { get; set; }
        public int Actions { get; set;} 
        public int Minutes { get; set; }
        public int Completions { get; set; }

        public bool IsFavorite { get; set; }
        public bool IsLike { get; set; }
    
    }

    /// <summary>
    /// Created to : Track story endings
    /// </summary>
    public class UserStoryEnding
    {
        public int ID { get; set; }


        public Story Story { get; set; }

        public Page Page { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public IdentityUser User { get; set; }


    }

}