using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Parth.Models;

namespace Parth.Controllers
{
    public class UsersController : Controller
    {
        private AddressBookEntities db = new AddressBookEntities();

        // GET: Users
        public ActionResult Index()
        {
            if (Session["Login"] != null)
            {
                return View();
            }
            Response.Redirect("Login");
            return View("Login");
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "Id,Name,Mobile,UserType,Email,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Login"]== null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            Response.Redirect("Login");
            return View("Login");
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Mobile,UserType,Email,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Login"] != null)
            {
                return View();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            Response.Redirect("Login");
            return View("Login");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Login()
        {
            Session["Login"]= null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(String Username, String Password)
        {

            if (db.Users.Where(x => (x.Username.Equals(Username)) && (x.Password.Equals(Password))).ToList().Count() == 1)
            {
                Session["Login"] = Username;
                if (db.Users.Where(y => (y.UserType.Equals("Admin")) && (y.Username.Equals(Username))).ToList().Count() == 1)
                {
                    return View("IndexAdmin");
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                Response.Write("Login Failed!!");
            }
            return View();
        }

        public ActionResult guide()
        {
            //if (Session["Login"] != null)
            //{
            //    return View();
            //}
            //Response.Redirect("Login");
            //return View("Login");
            return View();
        }
        public ActionResult Aboutus()
        {
            if (Session["Login"] != null)
            {
                return View();
            }
            Response.Redirect("Login");
            return View("Login");
        }
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public ActionResult Places()
        {
            return View();
        }
    }
}
