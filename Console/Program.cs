using System;
using System.Linq;
//using Business;
//using Business.Models;
//using Business.Repositories;
//using Consoles;
//using Data;
using ATConsole.Seed;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UI.DbContext;

namespace ATConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------- DATA SEEDING ROUTINE -----------");
            Console.WriteLine("This console program clears and seeds most database tables.");
            Console.WriteLine("Press <enter> to continue");
            //Console.ReadLine();
            new ElRunner().Exceute();

            Console.WriteLine("Press <enter> to exit");
            Console.ReadLine();



        }
    }

    public class KeyItemT<T>
    {
        public KeyItemT(int index, T item)
        {
            Index = index;
            Item = item;
        }
        public int Index { get; set; }
        public T  Item { get; set; }
    }

    public class DataFactory
    {
        //StoryRepository _sFactory;
        //private string _username;
        //public DataFactory(string username)
        //{
        //    _username = username;
        //   // _sFactory = new StoryRepository(username);

        //}

        //public static string[] Generes = {"Fariy Tails","Childrens Stories", "Horror", "Science Fiction", "Folk Ficition", "Superheroes", "Nightime"};

        //public StoryModel CreateStory()
        //{
        //    var genres = Generes.GetRandomValues(4).Select( t=> new GenreModel(){ID = t.Index, Text =  t.Item});

        //    var firstPage = new PageModel()
        //                       {
        //                           Body = "Once there was an only lady who lived in a shoe. She sneezed loudly and said ..... Then she took the dear old rabbit inside, she boiled the water and ..... the hide.",
        //                           IsEnding = true
        //                       };
        //    var story = new StoryEditModel()
        //                {
        //                    Title = "My Test Story",
        //                    Summary = "Once there was a rabbit",
        //                    FirstPage = firstPage,
        //                    Generes = genres
        //                };

        //   var result = _sFactory.CreateStory(story);
        //   return result;
        //}
    }
}
