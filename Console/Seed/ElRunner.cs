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

namespace ATConsole.Seed
{
    public class ElRunner
    {

        private static Random rand = new Random();

        public ElRunner(IdentityUser userID)
        {
            var dbContext = new AdventureTimeModel();
            dbContext.userID = userID;

            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {

                    //create tags 
                    var tags = new XmlParser("Tags.xml")
                        .ParseElements(t => new Tag()
                                            {
                                                Text = (string) t.XPathSelectElement("Name"),
                                                IsApproved = true
                                            })
                        .AddMetaData().ToList();

                    dbContext.Tags.AddRange(tags);
                    dbContext.SaveChanges();

                    //save tags
                    var savedTags = dbContext.Tags.Select(t => t).ToList();
                    var savedTagCount = savedTags.Count();

                    //create stories
                    var stories = new XmlParser("Stories.xml")
                        .ParseElements(t => new Story()
                            {
                                Title = (string) t.XPathSelectElement("Title"),
                                Summary = (string) t.XPathSelectElement("Summary"),
                                Settings = (string) t.XPathSelectElement("Settings"),
                                IsLocked = false
                            })
                        .AddMetaData().ToList();
                    stories.ForEach(t =>
                            {
                                var rep = rand.Next(2, 6);
                                t.Tags = new Collection<Tag>();
                                for (var ix = 0; ix < rep; ix++)
                                    t.Tags.Add(savedTags[rand.Next(0, savedTagCount-1)]);

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
                    var pages = new XmlParser("Pages.xml")
                        .ParseElements(t => new Page()
                            {
                                Text = (string) t.XPathSelectElement("Text"),
                                PageNumber = (int) t.XPathSelectElement("PageNumber"),
                                IsEnding = (bool) t.XPathSelectElement("IsEnding")
                            })
                        .AddMetaData()
                        .ToList();

                    var pageCount = pages.Count();
                    var pagesPerSomeStory = pageCount / someStoriesCount;
                    var prevStoryIx = -1;
                    for(var pIx = 0; pIx < pageCount; pIx++)
                    {
                        var page = pages.ElementAt(pIx);
                        var storyIx = pIx / pagesPerSomeStory;
                        page.Story = someStories[storyIx];

                        if (storyIx != prevStoryIx){
                            someStories[storyIx].FirstPage = page;
                        }
                        prevStoryIx = storyIx;

                        page.Text = page.Text.Substring(0,Math.Min(3000, page.Text.Length));
                    }
                    //save pages
                    dbContext.Pages.AddRange(pages);
                    dbContext.SaveChanges();

                    var savedPages = dbContext.Pages.Select(t => t);


                    //create actions in each story
                    var actions = new XmlParser("Actions.xml")
                        .ParseElements(t => new Action()
                                            {
                                                Text = (string) t.XPathSelectElement("Text")
                                            })
                        .AddMetaData().ToList();

                    //assign actions pages, story, etc.
                    var actionsCount = actions.Count();
                    var someStoryIx = 0;
                    var actionsPerStory = actionsCount/someStoriesCount;
                    foreach (var group in savedPages.GroupBy(t => t.Story))
                    {
                        var story = group.Key;
                        var someActions = actions.GetRange(actionsPerStory*someStoryIx, actionsPerStory).ToList();
                        someActions.ForEach(a =>
                            {
                                a.Story = story;
                                a.Text = a.Text.Substring(0, Math.Min(500, a.Text.Length));
                            });

                        var storyPages = group.Select(t => t).OrderBy(t => t.PageNumber).ToList();
                        var pagesPerStory = storyPages.Count();
                        var actionsPerPage = actionsPerStory/pagesPerStory;

                        for (var ix = 0; ix < actionsPerStory; ix++)
                        {
                            var action = someActions[ix];
                            action.ParentID = storyPages[(ix/actionsPerPage)].ID;
                            action.ChildID = storyPages[(((ix + 1)*2)/actionsPerPage)%pagesPerStory].ID;
                        }
                        //assign actions to pages

                        someStoryIx++;
                    }

                    dbContext.Actions.AddRange(actions);
                    dbContext.SaveChanges();
                    dbContextTransaction.Commit();

                  
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }

    }

    public static class MetaDataProvider
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

            //item.CreatedByUser = user;
            //item.ModifiedByUser = user;
            item.CreatedBy = userId;
            item.ModifiedBy = userId;
            item.CreatedOn = startDate;
            item.ModifiedOn = endDate;

            return item;
        }
    }
} 
