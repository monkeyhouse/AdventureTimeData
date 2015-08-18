using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using StructureMap;
using StructureMap.Graph;

namespace Test
{
    [TestClass]
    public class WhenCreateOrUpdateAStory
    {
        //private IStoryRepository repo;
        //private AdventureTimeModel dbContext;


        //[TestInitialize]
        //public void TestInit()
        //{
        //    dbContext = Common.GetContext();
        //    var container = Common.InitDependencyInjection(dbContext);
        //    repo = container.GetInstance<IStoryRepository>();
        //}

        //private StoryEditModel ScaffoldStory()
        //{
        //    //Arrange
        //    var genres = new List<GenreModel>(){
        //        new GenreModel { Text = "Horror"  },
        //        new GenreModel { Text = "Comedy" }
        //    };

        //    var firstPage = new PageModel { Body = "Segment Text" };

        //    var story = new StoryEditModel()
        //    {
        //        Title = "Test Title",
        //        Summary = "Test Byline",
        //        Generes = genres,
        //        FirstPage = firstPage
        //    };
        //    return story;
        //}

        //[TestMethod]
        //public void AbleToCreateStory()
        //{
        //    //Arrange
        //    var story = ScaffoldStory();

        //    //Act
        //    var result = repo.CreateStory(story);

        //    //Assert
        //    Assert.AreNotEqual(0, result.ID);
        //    Assert.AreNotEqual(0, result.FirstPage.ID);
        //    Assert.AreEqual(story.Title, result.Title);            
        //}

       
        //[TestMethod]
        //public void AbleToUpdateStory()
        //{
        //    var story = ScaffoldStory();
        //    var creation = repo.CreateStory(story);

        //    //Act
        //    story.Title = "Updated Title";
        //    story.FirstPage.ID = creation.FirstPage.ID;
        //    story.FirstPage.Body = "Updated Body";
        //    story.ID = creation.ID;

        //    var result = repo.UpdateStory(story);

        //    //Assert
        //    var verification = repo.GetStory(result.ID);
        //    Assert.IsNotNull(verification);
        //    Assert.AreEqual(verification.ID, story.ID);

        //    Assert.AreEqual(story.Title, result.Title);
        //    Assert.AreEqual(story.FirstPage.Body, result.FirstPage.Body);
           
        //    Assert.AreEqual(String.Join("",story.Generes.Select(t => t.Text)),
        //                    String.Join("",result.Generes.Select(t => t.Text)));
        //}

        //[TestMethod]
        //public void AbleToDeleteStory()
        //{
        //    //Arrange
        //    var storyEditModel = ScaffoldStory();
        //    var storyModel = repo.CreateStory(storyEditModel);
        //    var createdId = storyModel.ID;

        //    //Act
        //    var delResult = repo.DeleteStory(createdId);

        //    //Assert
        //    var count = dbContext.Stories.Count(t => t.ID == createdId);
        //    Assert.AreEqual(true, delResult);
        //    Assert.AreEqual(0, count);
        //}



    }
}
