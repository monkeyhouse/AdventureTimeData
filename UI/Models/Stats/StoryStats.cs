using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UI.Models.Stats
{
    // All are computed fields
    // values are maintained via calcualations after updates
    // can be synced with nighly runs if needed
    public class StoryStats
    {
        [Key, ForeignKey("Story")]
        public int StoryID { get; set; }

        //Cumulative User Stats
        public int Likes { get; set; }
        public int Favorites { get; set; }
        public int Plays { get; set; }
        public int Completions { get; set; }
        public int Views { get; set; }

        //intrensic properties
        public int Pages { get; set; }
        public int Actions { get; set; }
        public int Endings { get; set; }
        //public int Authors { get; set; }

        public virtual Story Story { get; set; }
    }

}