using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class UsersController : Controller
    {
        private DatabaseContext _db;

        public UsersController()
        {
            _db = new DatabaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }


        public ActionResult Index()
        {
            List<User> users = _db.Users.ToList();

            return View("Users", users);
        }

        public ActionResult UserProfile(int userID)
        {
            User user = _db.Users.SingleOrDefault(u => u.UserID == userID);
            return View("Details", user);
        }

        // Should be in own controller for UserListItem
        public ActionResult SavedList (int userID) 
        {   // Consistency with UserListItem, SavedList, "Favourites"?
            // First two are related, and this approach allows for future name changes ("Your Saved Items", "Your Favourites", "Your Bookmarks"...etc)
            ViewModel models = new ViewModel
            {
                User = _db.Users.SingleOrDefault(u => u.UserID == userID),
                Products = _db.Products
                    .Where(product => _db.UserListItems
                        .Any(listItem => listItem.UserID == userID // Only UserListItems matching User
                            && listItem.ProductID == product.ProductID)) // Only Products matching the products in that User's list
                    .Include(p => p.Category) // Ensure Category navigation property is loaded
                    .ToList(),
                
                UserListItems = _db.UserListItems
                    .Where(u =>  u.UserID == userID)
                    .ToList()
                    
            };

            return View("SavedList", models);
        }

        public void AddFavourite(int userID, int productID, string notes="")
        {
            bool itemExists = _db.UserListItems.Any(item => item.UserID == userID 
                                                    && item.ProductID == productID);

            if (!itemExists)
            {
                UserListItem newItem = new UserListItem
                {
                    UserID = userID,
                    ProductID = productID,
                    Notes = notes,
                    // Timestamp set automatically
                };

                _db.UserListItems.Add(newItem);
                _db.SaveChanges();
            }
            else
            {
                Debug.WriteLine($"Item already favourited by user {userID} ({productID}) - ignoring.");
            }
           
        }

        public void RemoveFavourite(int userID, int productID)
        {
            // Find the UserListItem to remove
            var userListItemToRemove = _db.UserListItems.FirstOrDefault(item => item.UserID == userID && item.ProductID == productID);

            if (userListItemToRemove != null)
            {
                // Remove the UserListItem from the database
                _db.UserListItems.Remove(userListItemToRemove);
                //_db.Entry(userListItemToRemove).State = EntityState.Deleted;
                _db.SaveChanges();
            }
            else
            {
                Debug.WriteLine($"Product {productID} was not found in User {userID}'s favourites - ignoring.");
            }
        }

        public bool IsFavourited(int userID, int productID)
        {
            return _db.UserListItems.Any(item => item.UserID == userID && item.ProductID == productID);
        }

        public void UpdateFavouriteNotes(int userID, int productID, string productNotes)
        {
            UserListItem userListItem = _db.UserListItems.FirstOrDefault(item => item.UserID == userID && item.ProductID == productID);
            if (userListItem != null)
            {
                userListItem.Notes = productNotes;
                _db.SaveChanges();
            }

        }

        public bool IsAdmin(int userID)
        {
            return _db.Users.Where(u => u.UserID == userID).Any(u => u.Role == Models.User.UserRole.Admin);
        }


        // Manage function name not matching view name (when running via view in IIS Express).
        public ActionResult Users() { return RedirectToAction("Index"); }

        /*
        public ActionResult Create()
        {
            return View("GadgetForm");
        }

        public ActionResult Edit(int id)
        {
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g => g.ID == id);
            return View("GadgetForm", gadget);
        }

        public ActionResult ProcessCreate(GadgetModel gadgetModel)
        {
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g => g.ID == gadgetModel.ID);

            if (gadget != null)
            // EDIT: Existing record found in database
            {
                gadget.Name = gadgetModel.Name;
                gadget.Description = gadgetModel.Description;
                gadget.AppearsIn = gadgetModel.AppearsIn;
                gadget.WithThisActor = gadgetModel.WithThisActor;

            }
            else
            // NEW: Record not found, add a new item
            {
                context.Gadgets.Add(gadgetModel);
            }

            // Update database
            context.SaveChanges();

            return View("Details", gadgetModel);
        }

        public ActionResult Delete(int id)
        {
            GadgetModel gadget = context.Gadgets.SingleOrDefault(g => g.ID == id);
            context.Entry(gadget).State = EntityState.Deleted;

            // Update database
            context.SaveChanges();

            GadgetModel deletedGadget = context.Gadgets.SingleOrDefault(g => g.ID == id);

            if (deletedGadget != null)
            {
                throw new InvalidOperationException("An error occured and the record was not deleted.");
            }

            return Redirect("/Gadgets");

        }

        */
    }
}