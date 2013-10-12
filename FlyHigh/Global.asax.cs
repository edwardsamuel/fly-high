using FlyHigh.Filters;
using FlyHigh.Models;
using MySql.Web.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FlyHigh
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            Database.SetInitializer<SimpleMembershipTestDbContext>(null);
            try
            {
                using (var context = SimpleMembershipTestDbContext.CreateContext())
                {
                    if (context.Database.Exists() == false)
                    {
                        // Create the SimpleMembership database without Entity Framework migration schema
                        ((IObjectContextAdapter) context).ObjectContext.CreateDatabase();
                    }
                }

                MySqlWebSecurity.InitializeDatabaseConnection("ErlanggaMySql");

                //const string ADMIN_ROLES = "Administrators";
                //const string ADMIN_USER = "admin";

                //if (System.Web.Security.Roles.RoleExists(ADMIN_ROLES) == false)
                //{
                //    System.Web.Security.Roles.CreateRole(ADMIN_ROLES);

                //    if (WebSecurity.UserExists(ADMIN_USER) == false)
                //        WebSecurity.CreateUserAndAccount(ADMIN_USER, "password");

                //    if (Array.Exists(System.Web.Security.Roles.GetRolesForUser(ADMIN_USER), f => f == ADMIN_ROLES) == false)
                //        System.Web.Security.Roles.AddUserToRole(ADMIN_USER, ADMIN_ROLES);
                //}
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }
        }
    }
}