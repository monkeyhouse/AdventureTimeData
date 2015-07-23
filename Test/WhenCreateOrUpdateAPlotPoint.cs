using System;
using System.Data.Entity;
using System.Linq;
using Business.Models;
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

//namespace Test
//{
//    [TestClass]
//    public class WhenCreateOrUpdateAPlotPoint
//    {
//        private IPlotPointRepository repo;
//        private AdventureTimeModel dbContext;
//        private Container container;

//        [TestInitialize]
//        public void TestInit()
//        {
//            dbContext = Common.GetContext();
//            container = Common.InitDependencyInjection(dbContext);
//            repo = container.GetInstance<IPlotPointRepository>();
//        }

//        private StoryModel CreateStory()
//        {
//            //Arrange
//            var genres = new List<GenreModel>(){ new GenreModel { Text = "Horror"  }, new GenreModel { Text = "Comedy" }};
//            var firstPage = new PageModel { Body = "Segment Text" };
//            var story = new StoryEditModel(){ Title = "Test Title", Summary = "Test Byline", Generes = genres, FirstPage = firstPage };
//            var storyEditModel = container.GetInstance<IStoryRepository>();
//            var result = storyEditModel.CreateStory(story);
//            return result;
//        }

//        [TestMethod]
//        public void AbleToCreatePlotPoint()
//        {
//            //Arrange
//            var story = CreateStory();
//            var body = new PageModel() { Body = "Body Text" };
//            var leader = new ActionModel() { Parent = story.FirstPage, Text = "Open the door", Child = body};
//            var model = new PlotPointAddModel { Leader = leader, Page = body };

//            //Act
//            var result = repo.CreatePlotPoint( model );

//            //Assert
//            Assert.AreEqual("Body Text", result.Page.Body);
//            Assert.AreNotEqual(0,result.Page.ID);
//            Assert.IsNotNull(result.Leaders);
//            Assert.AreNotEqual(0,result.Leaders.First().ID);
//        }

//        private PlotPointModel CreatePlotPoint()
//        {
//            var story = CreateStory();
//            var body = new PageModel() { Body = "Body Text" };
//            var leader = new ActionModel() { Parent = story.FirstPage, Text = "Open the door", Child = body };
//            var model = new PlotPointAddModel { Leader = leader, Page = body };
//            return repo.CreatePlotPoint(model);
//        }

//        public void AbleToUpdatePlotPointBody()
//        {
//            //Arrange
//            var plotPoint = CreatePlotPoint();

//            //Act
//            plotPoint.Page.Body = "Updated Body Text";
//            repo.UpdatePlotPoint(plotPoint);

//            //Assert

//        }

//        public void AbleToUpdatePlotPointLeaders()
//        {
            
//        }

//    }
//}
