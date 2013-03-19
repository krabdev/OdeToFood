using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        OdeToFoodDb _db = new OdeToFoodDb();

        //serachTerm param cause a user might come to home page with a serrach result
        public ActionResult Index(string searchTerm = null)
        {

            var model = 
                _db.Restaurants
                    .OrderByDescending( r => r.Reviews.Average ( review => review.Rating))
                    //.Take() and Skip() is used for pagination!
                    .Take(10)
                    //Check if the serachTerm is null, if not make a serach
                    .Where ( r => searchTerm == null || r.Name.StartsWith(searchTerm))
                    .Select( r => 
                        new RestaurantListViewModel
                             { 
                                 Id =r.Id,
                                 Name = r.Name,
                                 City =  r.City,
                                 Country = r.Country,
                                 CountOfReviews = r.Reviews.Count()
                             });

            return View(model);
        }

        public ActionResult About()
        {
            var query = from r in _db.Restaurants
                        where r.Country == "USA"
                        orderby r.Name
                        select r;
            var model = query.ToList();
            return View(model.First());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}