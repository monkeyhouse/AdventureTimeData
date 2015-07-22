using System;
using System.Linq;
using Business;
using Business.Repositories;
using Consoles;
using Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ATConsole
{
    public class Program
    {
        static void Main(string[] args)
        {

            //clear database

            string username = "sam";

            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var me = manager.FindByName("sam");
            if (me == null) manager.Create(new IdentityUser() { UserName = "sam", Email = "sam.prager@gmail.com" }, "password");

            var db = new AdventureTimeModel();


            Console.WriteLine("Press <enter> to clear tables");
            Console.ReadLine();

            Console.WriteLine("Clearing Database...");

            db.Database.ExecuteSqlCommand("Delete from StoryGenres where 1=1");
            db.Database.ExecuteSqlCommand("DELETE from Genres where 1=1");
            db.Database.ExecuteSqlCommand("DELETE from Stories where 1=1");
            db.Database.ExecuteSqlCommand("DELETE from Segments where 1=1");
            db.SaveChanges();


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
        StoryRepository _sFactory;
        private string _username;
        public DataFactory(string username)
        {
            _username = username;
           // _sFactory = new StoryRepository(username);

        }

        public static string[] Generes = {"Fariy Tails","Childrens Stories", "Horror", "Science Fiction", "Folk Ficition", "Superheroes", "Nightime"};

        public StoryModel CreateStory()
        {
            var genres = Generes.GetRandomValues(4).Select( t=> new GenreModel(){ID = t.Index, Text =  t.Item});

            var firstSegment = new SegmentModel()
                               {
                                   Leaders = null,
                                   Body = "Once there was an only lady who lived in a shoe. She sneezed loudly and said ..... Then she took the dear old rabbit inside, she boiled the water and ..... the hide.",
                                   Actions = null,
                                   IsEnding = true
                               };
            var story = new StoryEditModel()
                        {
                            Title = "My Test Story",
                            Byline = "Once there was a rabbit",
                            FirstSegment = firstSegment,
                            Generes = genres
                        };

           var result = _sFactory.CreateStory(story);
           return result;
        }
    }
}
