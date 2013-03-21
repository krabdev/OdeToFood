using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        OdeToFoodDb _db = new OdeToFoodDb();

        public ActionResult AutoComplete(string term)
        {
            var model =
                _db.Restaurants
                    .Where(r => r.Name.StartsWith(term))
                    .Take(10)
                    .Select(r => new
                    {
                        label = r.Name
                    });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //serachTerm param cause a user might come to home page with a serrach result
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            var model = 
                _db.Restaurants
                    .OrderByDescending( r => r.Reviews.Average ( review => review.Rating))
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
                             }).ToPagedList(page, 10);

            //If it's an ajax request from the search then just update the partial
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }
            //if not then present the full view
            return View(model);
        }

        [Authorize]
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