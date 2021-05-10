using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsIndiaMovieRatingApp.Models;

namespace AsIndiaMovieRatingApp.Controllers
{
    public class AccountController : Controller
    {
        private AsIndiaEntities db = new AsIndiaEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var count = db.Accounts.Count(a => a.Username.Equals(username) && a.Password.Equals(password));
            if(count>0)
            {
                Session["username"] = username;
                return RedirectToAction("Index", "Movie");
            }
            else
            {
                ViewBag.error = "Invalid Access !";
                return View("Login");
            }
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public ActionResult SignUp(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
            return RedirectToAction("Login","Account");
        }
    }
}





