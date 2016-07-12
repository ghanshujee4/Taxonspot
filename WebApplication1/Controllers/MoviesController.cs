using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxonspotMVC.Models;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using TaxonspotMVC.Filters;


namespace TaxonspotMVC.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        //
        // GET: /Movies/

        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }
        //
        // GET: /Movies/Details/5

        public ActionResult Details(int id = 0)
        {
            MovieDB moviedb = db.Movies.Find(id);
            if (moviedb == null)
            {
                return HttpNotFound();
            }
            return View(moviedb);
        }

        //
        // GET: /Movies/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Movies/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create(MovieDB moviedb)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(moviedb);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(moviedb);
        }

        //
        // GET: /Movies/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MovieDB moviedb = db.Movies.Find(id);
            if (moviedb == null)
            {
                return HttpNotFound();
            }
            return View(moviedb);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieDB moviedb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moviedb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moviedb);
        }

        //
        // GET: /Movies/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MovieDB moviedb = db.Movies.Find(id);
            if (moviedb == null)
            {
                return HttpNotFound();
            }
            return View(moviedb);
        }

        //
        // POST: /Movies/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieDB moviedb = db.Movies.Find(id);
            db.Movies.Remove(moviedb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult View1()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
        //    {
        //        return RedirectToAction(returnUrl);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Movies");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserDB u) { 
            if(ModelState.IsValid)
            {
                using(MovieDBContext mc = new MovieDBContext())
                {
                    var v = mc.Movies.Where(a => a.PAN.Equals(u.PAN) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null) {
                        Session["logedUserID"] = v.PAN.ToString();
                        Session["LogedUserFullname"] = v.Name.ToString();
                        return RedirectToAction("AfterLogin");
                    }
                }
            }
             ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(u);
        }
        
      
        //[AllowAnonymous]
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}

        //
        // POST: /Account/Login

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(UserDB model, string returnUrl)
        //{
        //    if (ModelState.IsValid && WebSecurity.Login(model.PAN, model.Password))
        //    {
        //        if (model != null)
        //        {
        //            //Session["logedUserID"] = model.PAN.ToString();
        //            //Session["LogedUserFullname"] = model.PAN.ToString();
        //            return RedirectToAction("AfterLogin");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
        //    return View(model);
        //}
        [AllowAnonymous]
        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}