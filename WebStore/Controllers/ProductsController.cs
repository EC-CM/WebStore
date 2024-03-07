using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

using WebStore.Models;
using System.Diagnostics;


// NEXT TASK: Search bar functionality.
/*
 * Cart icon with badge for item count
 * Favourties
 * Work out how this will fit into the navbar
 * Centre search independent of logo?
 * Welcome username in navbar?
 */


namespace WebStore.Controllers
{

    public class ProductsController : Controller
    {
        private DatabaseContext _db;

        public ProductsController()
        {
            _db = new DatabaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }


        // GET: Products
        public ActionResult Index(string searchPhrase = null, int category = 0, bool sort = false)
        {   // Note: URL params = ?var, so should maybe be descriptive.
            // Change view to grid/list/compact list

            ViewModel models = new ViewModel
            {
                Products = _db.Products.ToList(),
            };

            if (searchPhrase != null)
            { 
                if (searchPhrase == "")
                // Essentially just refresh the current page
                { return Redirect(Request.UrlReferrer.ToString()); }

                models = Search(models, searchPhrase);
            }

            if (category != 0)
            { models = FilterByCategory(models, category);}

            else
            { models.Categories = _db.Categories.ToList(); }

            if (sort)
            { models = Sort(models); }

            return View("Products", models);
        }

        public ActionResult Products()
        {
            return RedirectToAction("Index");
        }



        public ActionResult Details(int productID = 0)
        {
            ViewModel models = new ViewModel
            {
                Product = _db.Products.FirstOrDefault(p => p.ProductID == productID),
                ProductImages = _db.ProductImages
                    .Where(pi => pi.ProductID == productID)
                    .ToList()
                
            };

            models.Category = _db.Categories.FirstOrDefault(c => c.CategoryID == models.Product.CategoryID);

            return View("ProductDetails", models);
            
        }

        public ViewModel Search(ViewModel vm, string searchPhrase)
        {
            vm.SearchPhrase = searchPhrase;

            if (searchPhrase.StartsWith("\"") && searchPhrase.EndsWith("\""))
            // Exact match: "searchPhrase"
            {
                // Strip enclosing quotes and enclose in spaces to allow for detecting word boundaries
                searchPhrase = $" {searchPhrase.Substring(1, searchPhrase.Length - 2)} ";

                vm.Products = vm.Products
                    .Where(product => (" " + product.Name + " ")
                        .Contains(searchPhrase))
                    .ToList();
            }
            else
            // General match
            {
                vm.Products = vm.Products
                    .Where(product => product.Name.ToLower()
                        .Contains(searchPhrase.ToLower()))
                    .ToList();
            }

            return vm;
        }


        public ActionResult SearchInNewPage(string searchPhrase)
        // NOTE: Use prepared statements
        {
            ViewModel products = new ViewModel { };
            
            if (searchPhrase.StartsWith("\"") && searchPhrase.EndsWith("\""))
            // Exact match: "searchPhrase"
            {
                // Strip enclosing quotes and enclose in spaces to allow for detecting word boundaries
                searchPhrase = $" {searchPhrase.Substring(1, searchPhrase.Length - 2)} ";

                // 
                products.Products = (from p in _db.Products
                           where (" " + p.Name + " ").Contains(searchPhrase)
                           select p).ToList();

            }
            else
            // General match
            {
                products.Products = (from p in _db.Products
                          where p.Name.Contains(searchPhrase)
                          select p).ToList();
            }

            return View("Products", products);
        }

        public ViewModel FilterByCategory(ViewModel vm, int categoryID)
        {
            vm.Products = vm.Products
                .Where(product => product.CategoryID == categoryID)
                .ToList();

            vm.Category = _db.Categories.FirstOrDefault(c => c.CategoryID == categoryID);

            return vm;
        }

        public ViewModel Sort(ViewModel vm)
        {
            vm.Products = vm.Products.OrderBy(product => product.Price).ToList();
            return vm;
        }
    }
}
