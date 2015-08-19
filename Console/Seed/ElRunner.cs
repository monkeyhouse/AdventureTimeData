using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.XPath;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UI.DbContext;
using UI.Models;
using Action = UI.Models.Action;
using ATConsole.Seed.Pocos;
using UI.Models.Stats;
using Microsoft.Practices.ObjectBuilder2;

namespace ATConsole.Seed
{
    public class ElRunner
    {

        private static Random rand = new Random();

        public void Exceute()
        {
            //clear tables before create root user
            ClearTables();

            //creat root user
            var rootUser = CreateRootUser();

            //seed tables with sample data
            SeedTables(rootUser);          
        }

        private void SeedTables(IdentityUser rootUser)
        {
            //create db context
            using (var dbContext = new AdventureTimeDbContext())
            {
                dbContext.userID = rootUser;
                dbContext.AutoSeedMetadata = false;

                using (var dbContextTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Console.WriteLine("Seeding Users...");
                        //create users
                        var userDtos = new XmlParser("Users.xml")
                           .ParseElements(t => new UserDTO()
                           {
                               UserName = (string)t.XPathSelectElement("UserName"),
                               Password = (string)t.XPathSelectElement("Password"),
                               Email = (string)t.XPathSelectElement("Email")
                           }).ToList();
                        var users = new List<IdentityUser>();
                        var manager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                        foreach (var u in userDtos)
                        {
                            if (manager.FindByName(u.UserName) == null)
                            {
                                manager.Create(new IdentityUser() { UserName = u.UserName, Email = u.Email }, u.Password);
                                users.Add(manager.FindByName(u.UserName));
                            }
                        }

                        Console.WriteLine("Seeding Tags...");
                        //create tags 
                        var tags = new XmlParser("Tags.xml")
                            .ParseElements(t => new Tag()
                            {
                                Text = (string)t.XPathSelectElement("Name"),
                                IsApproved = true
                            })
                            .AddMetaData().ToList();

                        dbContext.Tags.AddRange(tags);
                        dbContext.SaveChanges();

                        //save tags
                        var savedTags = dbContext.Tags.ToList();
                        var savedTagCount = savedTags.Count();


                        Console.WriteLine("Seeding Stories...");
                        //create stories
                        var stories = new XmlParser("Stories.xml")
                            .ParseElements(t => new Story()
                            {
                                Title = (string)t.XPathSelectElement("Title"),
                                Summary = (string)t.XPathSelectElement("Summary"),
                                Settings = (string)t.XPathSelectElement("Settings"),
                                IsLocked = false
                            })
                            .AddMetaData().ToList();
                        stories.ForEach(t =>
                        {
                            var rep = rand.Next(2, 6);
                            t.Tags = new Collection<Tag>();
                            for (var ix = 0; ix < rep; ix++)
                                t.Tags.Add(savedTags[rand.Next(0, savedTagCount - 1)]);

                            //trim summary,settings
                            t.Summary = t.Summary.Substring(0, Math.Min(500, t.Summary.Length));
                            t.Settings = t.Settings.Substring(0, Math.Min(250, t.Settings.Length));
                        });

                        //save stories
                        dbContext.Stories.AddRange(stories);
                        dbContext.SaveChanges();

                        //feature stories to seed pages for
                        const int someStoriesCount = 4;
                        var someStories = dbContext.Stories.Take(someStoriesCount).ToArray();

                        // create pages
                        //depends on stories
                        Console.WriteLine("Seeding Pages...");
                        var pages = new XmlParser("Pages.xml")
                            .ParseElements(t => new Page()
                            {
                                Text = (string)t.XPathSelectElement("Text"),
                                PageNumber = (int)t.XPathSelectElement("PageNumber"),
                                IsEnding = false // (bool)t.XPathSelectElement("IsEnding")
                            })
                            .AddMetaData()
                            .ToList();

                        var pageCount = pages.Count();
                        var pagesPerSomeStory = pageCount / someStoriesCount;
                        var prevStoryIx = -1;
                        var endingChance = 0.00;
                        for (var pIx = 0; pIx < pageCount; pIx++)
                        {
                            var page = pages.ElementAt(pIx);
                            var storyIx = pIx / pagesPerSomeStory;
                            page.Story = someStories[storyIx];

                            if (storyIx != prevStoryIx)
                            {
                                endingChance = 0.00;
                                someStories[storyIx].FirstPage = page;
                            }
                            else
                            {
                                //chance of hitting ending increases towards end of story
                                endingChance += (.25 / (double)pagesPerSomeStory);
                                page.IsEnding = rand.NextDouble() < endingChance * endingChance;
                            }
                            prevStoryIx = storyIx;

                            page.Text = page.Text.Substring(0, Math.Min(3000, page.Text.Length));
                        }
                        //save pages
                        dbContext.Pages.AddRange(pages);
                        dbContext.SaveChanges();

                        var savedPages = dbContext.Pages.Select(t => t);


                        //create actions in each story
                        Console.WriteLine("Seeding Actions...");
                        var actions = new XmlParser("Actions.xml")
                            .ParseElements(t => new Action()
                            {
                                Text = (string)t.XPathSelectElement("Text")
                            })
                            .AddMetaData().ToList();

                        //assign actions pages, story, etc.
                        var actionsCount = actions.Count();
                        var someStoryIx = 0;
                        var actionsPerStory = actionsCount / someStoriesCount;
                        var rem = actionsCount % someStoriesCount;
                        foreach (var group in savedPages.GroupBy(t => t.Story))
                        {
                            var story = group.Key;
                            var someActions = actions.GetRange(
                                        actionsPerStory * someStoryIx,
                                        actionsPerStory + ((someStoryIx == someStoriesCount - 1) ? rem : 0)).ToList();
                            someActions.ForEach(a =>
                            {
                                a.Story = story;
                                a.Text = a.Text.Substring(0, Math.Min(500, a.Text.Length));
                            });

                            var storyPages = group.Select(t => t).OrderBy(t => t.PageNumber).ToList();
                            var pagesPerStory = storyPages.Count();
                            var actionsPerPage = actionsPerStory / pagesPerStory;

                            var actionsPerThisStory = someActions.Count;
                            for (var ix = 0; ix < actionsPerThisStory; ix++)
                            {
                                var action = someActions[ix];
                                var childPage = storyPages[(((ix + 1) * 2) / actionsPerPage) % pagesPerStory];
                                var parentPage = storyPages[(ix / actionsPerPage)];
                                if (parentPage.IsEnding)
                                {
                                    //no action
                                }
                                else
                                {
                                    //assign action to pages
                                    action.ParentID = parentPage.ID;
                                    action.ChildID = childPage.ID;
                                }
                            }

                            someStoryIx++;
                        }

                        //remove actions that did not get parent assigned
                        actions = actions.Where(t => t.ParentID != null).ToList();
                        dbContext.Actions.AddRange(actions);
                        dbContext.SaveChanges();
                        dbContextTransaction.Commit();


                        Console.WriteLine("Seeding Playing Stats...");
                        //Seed User Stats, User Story Stats
                        var usersCount = users.Count();
                        List<UserStoryStats> userStoryStats = new List<UserStoryStats>();
                        List<StoryStats> storyStats = new List<StoryStats>();
                        List<UserStoryEnding> userStoryEndings = new List<UserStoryEnding>();

                        for (var i = 0; i < someStories.Length; i++)
                        {
                            var story = someStories[i];
                            var endings = story.Pages.Where(t => t.IsEnding);
                            var endingsCount = endings.Count();

                            var thisUserStoryStats = new List<UserStoryStats>();

                            for (var u = 0; u < usersCount; u++)
                            {

                                var hasPlayed = rand.NextDouble() < .4;
                                if (!hasPlayed) continue;

                                var user = users[u];

                                var userStat = new UserStoryStats
                                {
                                    IsLike = rand.NextDouble() < .2,
                                    IsFavorite = rand.NextDouble() < .1,
                                    Minutes = rand.Next(3, 10),
                                    Plays = rand.Next(1, 3),
                                    Completions = rand.Next(1, 4),
                                    Pages = rand.Next(1, 200),
                                    Actions = rand.Next(2, 200),
                                    UserID = user.Id,
                                    Story = story
                                };


                                if (endingsCount > 0)
                                {
                                    for (var c = 0; c < userStat.Completions; c++)
                                    {
                                        var page = endings.ElementAt(rand.Next(0, endingsCount));
                                        userStoryEndings.Add(new UserStoryEnding()
                                        {
                                            UserID = user.Id,
                                            Story = story,
                                            Page = page
                                        });
                                    }
                                }
                                thisUserStoryStats.Add(userStat);
                            }

                            var storyStat = new StoryStats
                            {
                                Story = story,
                                Pages = story.Pages.Count(),
                                Actions = story.Actions.Count(),
                                Likes = thisUserStoryStats.Count(t => t.IsLike),
                                Favorites = thisUserStoryStats.Count(t => t.IsFavorite),
                                Plays = thisUserStoryStats.Sum(t => t.Plays),
                                Completions = thisUserStoryStats.Sum(t => t.Completions),
                                Views = thisUserStoryStats.Count(),
                                Endings = endingsCount
                            };

                            userStoryStats.AddRange(thisUserStoryStats);
                            storyStats.Add(storyStat);
                        }

                        dbContext.UserStoryEndings.AddRange(userStoryEndings);
                        dbContext.StoryStats.AddRange(storyStats);
                        dbContext.UserStoryStats.AddRange(userStoryStats);
                        dbContext.SaveChanges();
    
                        //add story stats for remaining stories
                        var emptyStats = new List<StoryStats>();
                        var someIds = new HashSet<int>(someStories.Select(t => t.ID));
                        dbContext.Stories.Where(s => !someIds.Contains(s.ID))
                        .ForEach(t => {
                            var stat = new StoryStats() { Story = t };
                            emptyStats.Add(stat);
                        });
                        dbContext.StoryStats.AddRange( emptyStats );

                        dbContext.SaveChanges();




                }
                    catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            } //end transaction
            }//end db context
        }


        /// <summary>
        /// Clear most tables.
        /// </summary>
        /// <remarks>
        /// Climbs the dependency chain clearing leafs first
        /// </remarks>
        private void ClearTables()
        {
            using (var db = new AdventureTimeDbContext())
            {
                db.Database.ExecuteSqlCommand("DELETE from StoryStats where 1=1");
                db.Database.ExecuteSqlCommand("DELETE from UserStoryStats where 1=1");
                db.Database.ExecuteSqlCommand("DELETE from UserStoryEndings where 1=1");
                db.Database.ExecuteSqlCommand("DELETE from StoryTags where 1=1");
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("DELETE from Actions where 1=1");
                db.Database.ExecuteSqlCommand("DELETE from Tags where 1=1");
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("UPDATE Stories set FirstPageId = null where 1=1");
                db.Database.ExecuteSqlCommand("DELETE from Pages where 1=1");
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("DELETE from Stories where 1=1");
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("DELETE from AspNetUserClaims where 1=1");
                db.Database.ExecuteSqlCommand("DELETE from AspNetUserRoles where 1=1");
                db.Database.ExecuteSqlCommand("DELETE from AspNetUserLogins where 1=1");
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("DELETE from AspNetUsers where 1=1");
                db.Database.ExecuteSqlCommand("DELETE from AspNetRoles where 1=1");
                db.SaveChanges();
            }
        }


        private IdentityUser CreateRootUser()
        {
            //create user me
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var me = manager.FindByName("sam");
            if (me == null)
            {
                manager.Create(new IdentityUser() { UserName = "sam", Email = "sam.prager@gmail.com" }, "password");
                me = manager.FindByName("sam");
            }

            return me;
        }
    }

    /// <summary>
    /// Creates random Metadata for records
    /// </summary>
    internal static class MetaDataProvider
    {
        private static string userId;
        private static IdentityUser user;
        private static Random rand = new Random();
        static MetaDataProvider()
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var me = manager.FindByName("sam");

            user = me;
            userId = me.Id;
        }

        public static IEnumerable<T> AddMetaData<T>(this IEnumerable<T> items) where T : Metadata
        {
            return items.Select( AddMetaData );
        }

        public static T AddMetaData<T>(this T item) where T : Metadata
        {

            var backDays = rand.Next(10, 1000);
            var forwardDays = rand.Next(0, backDays);

            var startDate = DateTime.Today.AddDays(-1 * backDays);
            var endDate = startDate.AddDays(forwardDays);

            item.CreatedOn = startDate;
            item.ModifiedOn = endDate;
            item.CreatedBy = userId;
            item.ModifiedBy = userId;

            //item.CreatedByUser = user;
            //item.ModifiedByUser = user;

            return item;
        }
    }
} 
