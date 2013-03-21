namespace OdeToFood.Migrations
{
    using OdeToFood.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFood.Models.OdeToFoodDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OdeToFood.Models.OdeToFoodDb context)
        {
            context.Restaurants.AddOrUpdate(r => r.Name,
                new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
                new Restaurant { Name = "Great Lake", City = "Chicago", Country = "USA" },
                new Restaurant { Name = "Andrea's Fast Food", City = "Nicosia", Country = "Cyprus"},
                new Restaurant { Name = "Lime", City = "Palaichori", Country = "Cyprus" },

                new Restaurant
                {
                    Name = "Smaka",
                    City = "Gothenburg",
                    Country = "Sweden",
                    Reviews =
                        new List<RestaurantReview> {
                                new RestaurantReview { Rating = 9, Body = "Great Food!", ReviewerName = "Scott" },
                             
                        }
                });

            for (int i = 1; i < 1000; i++)
            {
                context.Restaurants.AddOrUpdate(r => r.Name,
                    new Restaurant { Name = i.ToString(), City="Nowhere", Country="Someplace" });
            }

            SeedMembeship();
        }

        private void SeedMembeship()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("krabos1", false) == null)
            {
                membership.CreateUserAndAccount("krabos1", "krabos666");
            }
            if (!roles.GetRolesForUser("krabos1").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "krabos1" }, new[] { "admin" });
            }
           
        }
    }
}
