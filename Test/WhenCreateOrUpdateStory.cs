using System;
using System.Data.Entity;
using System.Linq;
using Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using System.Collections.Generic;
using Business.Repositories;
using StructureMap;
using StructureMap.Graph;

namespace Test
{
    [TestClass]
    public class WhenCreateOrUpdateAStory
    {
        private IStoryRepository repo;
        private AdventureTimeModel dbContext;


        [TestInitialize]
        public void TestInit()
        {          
            var db = new AdventureTimeModel();
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            db.userID = userManager.FindByName("sam");

            var container = new Container(_ =>
            {


                _.Policies.FillAllPropertiesOfType<AdventureTimeModel>().Use(db);

                _.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.Assembly("Business");
                    x.WithDefaultConventions();
                    x.SingleImplementationsOfInterface();

                });

               
            });

            repo = container.GetInstance<IStoryRepository>();

            //each test
            dbContext = db;

        }

        private StoryEditModel ScaffoldStory()
        {
            //Arrange
            var genres = new List<GenreModel>(){
                new GenreModel { Text = "Horror"  },
                new GenreModel { Text = "Comedy" }
            };

            var firstSegment = new SegmentModel { Body = "Segment Text" };

            var story = new StoryEditModel()
            {
                Title = "Test Title",
                Byline = "Test Byline",
                Generes = genres,
                FirstSegment = firstSegment
            };
            return story;
        }

        [TestMethod]
        public void AbleToCreateStory()
        {
            var story = ScaffoldStory();

            var result = repo.CreateStory(story);

            Assert.AreNotEqual(0, result.ID);
            Assert.AreNotEqual(0, result.FirstSegment.ID);
            Assert.AreEqual(story.Title, result.Title);            
        }

       
        [TestMethod]
        public void AbleToUpdateStory()
        {
            var story = ScaffoldStory();
            var creation = repo.CreateStory(story);

            //Act
            story.Title = "Updated Title";
            story.FirstSegment.ID = creation.FirstSegment.ID;
            story.FirstSegment.Body = "Updated Body";
            story.ID = creation.ID;

            var result = repo.UpdateStory(creation.ID, story);

            //Assert
            var verification = repo.GetStory(result.ID);
            Assert.IsNotNull(verification);
            Assert.AreEqual(verification.ID, story.ID);

            Assert.AreEqual(story.Title, result.Title);
            Assert.AreEqual(story.FirstSegment.Body, result.FirstSegment.Body);
           
            Assert.AreEqual(String.Join("",story.Generes.Select(t => t.Text)),
                            String.Join("",result.Generes.Select(t => t.Text)));
        }

        [TestMethod]
        public void AbleToDeleteStory()
        {
            var storyEditModel = ScaffoldStory();
            var storyModel = repo.CreateStory(storyEditModel);
            var createdId = storyModel.ID;

            var delResult = repo.DeleteStory(createdId);

            var count = dbContext.Stories.Count(t => t.ID == createdId);
            Assert.AreEqual(0, count);
        }



    }
}
