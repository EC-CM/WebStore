using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;
using WebStore.Services;
using System.Diagnostics;
using PasswordHasher = BCrypt.Net.BCrypt;

namespace WebStore.Controllers
{
    public class UserLoginsController : Controller
    {
        private DatabaseContext _db;

        public UserLoginsController()
        {
            _db = new DatabaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();

        }

        // GET: UserLogins
        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult ViewPasswordHash(string password)
        {
            return Content(PasswordHasher.HashPassword(password));
        }

        // POST: UserLogins
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            // Input data from form was valid for LoginViewModel
            {
                User user = _db.Users.FirstOrDefault(u => u.Username == model.Username);

                if (user == null)
                // Invalid username
                {
                    Debug.WriteLine($"Input username ({model.Username}) was not found in Users.");
                    return View(model);
                }
                else
                {
                    if (!PasswordHasher.Verify(model.Password, user.PasswordHash))
                    // Invalid password
                    {
                        Debug.WriteLine($"Input password's hash did not match database record for {user.Username}.");
                        return View(model);
                    }
                    else
                    // Successful login
                    {
                        if (LoginUser(user.UserID)) { return RedirectToAction("Index", "Home"); }
                        else { return Content("An error occured during the login process - please check the debug console."); }
                        
                    }

                }
            }
            else
            // Validation checks failed
            {
                return View(model);
            }
        }

        public bool LoginUser(int userID)
        {
            // Check if a user is not already logged in
            if (CurrentUserID() == 0)
            {
                UserLoginSession userLogin = _db.UserLoginSessions.SingleOrDefault(login => login.UserID == userID);
                
                if (userLogin != null)
                // Update found record with "logged in" status
                { 
                    userLogin.Active = true;
                }
                else
                // No existing record found - create and set as "logged in"
                {
                    userLogin = new UserLoginSession { UserID = userID, Active = true };
                    _db.UserLoginSessions.AddOrUpdate(userLogin);
                }

                _db.SaveChanges();
                return true;
            }
            else
            {
                Debug.WriteLine($"Another user is already logged in: {CurrentUser().Username}.");
                return false;
            }
        }

        public void LogOutUser()
        {
            // Get ID of logged in user
            int currentUserID = CurrentUserID();

            if (currentUserID != 0)
            // A logged in user was found
            {
                _db.UserLoginSessions.SingleOrDefault(login => login.UserID == currentUserID)
                    .Active = false; // Set status to "logged out"

                _db.SaveChanges();
                //return true;
            }
            else
            {
                Debug.WriteLine($"No logged in users were found to log out.");
                //return false;
            }
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