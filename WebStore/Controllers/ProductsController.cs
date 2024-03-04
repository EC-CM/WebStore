using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

using WebStore.Models;

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
        public ActionResult Index(int filterByCategory = 0, bool sort = false)
        {
            ViewModel models = new ViewModel
            {
                Products = _db.Products.ToList(),
            };

            if (filterByCategory != 0)
            {
                models = FilterByCategory(models, filterByCategory);
                models.Category = _db.Categories.FirstOrDefault(c => c.CategoryID == filterByCategory);
            }
            else
            {
                models.Categories = _db.Categories.ToList();
            }

            if (sort)
            {
                models = Sort(models);
            }

            return View("Index", models);
        }



        public ActionResult Details(int productID = 0)
        {
            ViewModel models = new ViewModel
            {
                Product = _db.Products.FirstOrDefault(p => p.ProductID == productID)
            };

            models.Category = _db.Categories.FirstOrDefault(c => c.CategoryID == models.Product.CategoryID);

            // Maybe just pass in Product?
            return View("ProductDetails", models);
            
        }

        public ViewModel FilterByCategory(ViewModel vm, int categoryID)
        {
            vm.Products = vm.Products
                .Where(product => product.CategoryID == categoryID)
                .ToList();

            return vm;
        }

        public ViewModel Sort(ViewModel vm)
        {
            vm.Products = vm.Products.OrderBy(product => product.Price).ToList();
            return vm;
        }
    }
}
