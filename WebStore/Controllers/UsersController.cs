using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public ActionResult Users()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Profile(int userID)
        {
            User user = _db.Users.SingleOrDefault(u => u.UserID == userID);
            return View("Details", user);
        }


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