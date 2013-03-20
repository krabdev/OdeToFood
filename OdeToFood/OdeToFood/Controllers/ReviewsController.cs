using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class ReviewsController : Controller
    {

        //
        // GET: /Reviews/
        OdeToFoodDb _db = new OdeToFoodDb();

        //Binder here make id to be showned and treated as restaurantID
        public ActionResult Index([Bind(Prefix="id")] int restaurantId)
        {
            var restaurant = _db.Restaurants.Find(restaurantId);
            if (restaurant != null)
            {
                return View(restaurant);
            }
            return HttpNotFound();
        }

        //get
        //Reviews/Edit

        [HttpGet]
        public ActionResult Create(int restaurantId)
        {
            return View();
        }

        //post
        //Reviews/Edit

        [HttpPost]
        public ActionResult Create(RestaurantReview review)
        {
            if (ModelState.IsValid)
            {
                _db.RestaurantReviews.Add(review);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }
            //if we come here that means somethink is wrong!
            return View(review);
        }

        //get
        //Review/edit

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _db.RestaurantReviews.Find(id);
            return View(model);
        }

        //post
        //review/post

        [HttpPost]
        public ActionResult Edit(RestaurantReview review)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(review).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }
            return View(review);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
