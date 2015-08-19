using System.Web.Http;

namespace UI
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services 

            // Web API routes 
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "StoryStats",
               routeTemplate: "api/{controller}/{storyID}/{action}/{pageID}",
               defaults: new { pageID = RouteParameter.Optional },
               constraints : new { controller="StoryStats", storyID=@"\d+", pageID =@"\d*" }          
            );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );


        } 
    }
}