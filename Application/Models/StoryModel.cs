﻿using System;
using System.Collections.Generic;
using System.Linq;
using Business;
using Business.Models;

namespace Business
{
    public class StoryModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public IEnumerable<GenreModel> Generes { get; set; }
        public PageModel FirstPage { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0} Title: {1} \n Byline:{2} \n Genres:[{3}] FirstPage.Body:{4}", ID, Title, Summary, string.Join(",", Generes.Select(t=> t.Text)), FirstPage.Body);
        }
    }

    public class StoryEditModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public IEnumerable<GenreModel> Generes { get; set; }
        public PageModel FirstPage { get; set; }
    }
}