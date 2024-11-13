using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer(
    new MvcMusicStore.Models.SampleData());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CreateRolesAndAdminUser();
        }

        private void CreateRolesAndAdminUser()
        {
            if (!Roles.RoleExists("Administrator"))
            {
                // Create the Administrator role
                Roles.CreateRole("Administrator");

                // Create a default admin user
                if (Membership.GetUser("adminUser") == null)
                {
                    Membership.CreateUser("adminUser", "Admin@123", "admin@example.com");
                    Roles.AddUserToRole("adminUser", "Administrator");
                }
            }
        }
    }
}
