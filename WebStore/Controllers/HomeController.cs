﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using System.IO;
using Size = System.Drawing.Size;
using Img = System.Drawing.Image;
using Bitmap = System.Drawing.Bitmap;
using WebStore.Models;
using System.Diagnostics;

using System.Web;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using System.Data.SqlTypes;
using System.Runtime.Remoting.Contexts;

/* TO DO:
 * - Implement cropping image behaviour?
 * - More comments
 * - GetImage: The IF can come before the USINGs for resizing.
 */

/* NOTES:
 * Resource Usage: Creating a new instance of ApplicationDbContext for each request might result in increased resource usage. The context is typically designed to be reused across multiple requests, and managing its lifecycle carefully can be beneficial for performance.
 * Connection Pooling: If your ApplicationDbContext is configured to use a database connection, creating a new instance for each request might impact connection pooling. Connection pooling is an optimization technique where connections are reused rather than closed and reopened for each request.
 * Testing: Creating a new instance directly within the constructor can make it challenging to mock or substitute the ApplicationDbContext during unit testing. If unit testing is a concern, you might want to consider dependency injection and constructor injection for better testability.
 */

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext _db;

        public HomeController()
        {
            _db = new DatabaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }

        public ActionResult Index()
        {
            ViewModel models = new ViewModel
            {
                Categories = _db.Categories.ToList()
            };

            return View("Home", models);
        }

        public ActionResult Home()
        {
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ASPNET_Guide()
        {
            return View("ASPNET_Guide");
        }


        public User CurrentUser()
        {
            // Throw exception if multiple logged in users are found
            User currentUser = _db.Users.SingleOrDefault(user => _db.UserLoginSessions
                .Any(login => login.UserID == user.UserID
                     && login.Active == true));

            if (currentUser == null)
            // No user logged in - return defaults
            { return new User(); }
            else
            // Return logged in user
            { return currentUser; }

        }

        public int CurrentUserID()
        {
            return CurrentUser().UserID;
        }
    }
}