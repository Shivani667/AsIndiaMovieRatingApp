using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsIndiaMovieRatingApp.Models;

namespace AsIndiaMovieRatingApp.Controllers
{
    public class MovieController : Controller
    {
        private AsIndiaEntities db = new AsIndiaEntities();

        public ActionResult Index()
        {
            ViewBag.movies = db.Movies.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var movie = db.Movies.Find(id);
            ViewBag.movie = movie;
            var review = new Review()
            {
                MovieId = movie.Id
            };
            return View("Detail", review);
        }
        [HttpPost]
       public ActionResult SendReview(Review review , double rating)
        {
            string username = Session["username"].ToString();
            review.DatePost = DateTime.Now;
            review.UserId = db.Accounts.Single(a => a.Username.Equals(username)).Id;
            review.Rating = rating;
            db.Reviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("Detail", "Movie" , new { id = review.MovieId});
        }
    }
}