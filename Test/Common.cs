
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StructureMap;
using StructureMap.Graph;

namespace Test
{
    internal static class Common
    {
        //public static AdventureTimeModel GetContext()
        //{
        //    var db = new AdventureTimeModel();
        //    var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        //    db.userID = userManager.FindByName("sam");
        //    return db;
        //}

        //public static Container InitDependencyInjection(AdventureTimeModel db)
        //{
        //    var container = new Container(_ =>
        //    {


        //        _.Policies.FillAllPropertiesOfType<AdventureTimeModel>().Use(db);

        //        _.Scan(x =>
        //                {
        //                    x.TheCallingAssembly();
        //                    x.Assembly("Business");
        //                    x.WithDefaultConventions();
        //                    x.SingleImplementationsOfInterface();

        //                });


        //    });

        //    return container;
        //}

    }
}
