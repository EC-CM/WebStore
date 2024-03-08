using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class UserCartsController : Controller
    {
        private DatabaseContext _db;

        public UserCartsController()
        {
            _db = new DatabaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();

        }

        public ActionResult Index()
        {
            // View("Cart");
            return View();
        }

        public void AddToCart(int userID, int productID)
        {
            UserCart activeCart = _db.UserCarts.FirstOrDefault(cart => cart.UserID == userID
                                                                && cart.Active == true);

            if (activeCart != null)
            {
                CartProduct newItem = new CartProduct
                {
                    UserCartID = activeCart.UserCartID,
                    ProductID = productID,
                    Quantity = 1
                };

                _db.CartProducts.Add(newItem);
                _db.SaveChanges();
            }
            else
            {
                Debug.WriteLine($"Error: Could not find an active cart for User {userID}.");
            }

        }

        public void RemoveFromCart(int userID, int productID)
        {
            UserCart activeCart = _db.UserCarts.FirstOrDefault(cart => cart.UserID == userID
                                                                && cart.Active == true);

            if (activeCart != null)
            {
                CartProduct itemToRemove = _db.CartProducts.FirstOrDefault(item => item.UserCartID == activeCart.UserCartID && item.ProductID == productID);

                if (itemToRemove != null)
                {
                    _db.CartProducts.Remove(itemToRemove);
                    _db.SaveChanges();
                }
                else
                {
                    Debug.WriteLine($"Error removing Product {productID} from Cart {activeCart.UserCartID}.");
                }
            }
            else
            {
                Debug.WriteLine($"Error: Could not find an active cart for User {userID}.");
            }

        }

        public void EditCartItem(int userID, int productID, int quantity)
        {
            UserCart activeCart = _db.UserCarts.FirstOrDefault(cart => cart.UserID == userID
                                                                && cart.Active == true);

            if (activeCart != null)
            {
                CartProduct itemToEdit = _db.CartProducts.FirstOrDefault(item => item.UserCartID == activeCart.UserCartID && item.ProductID == productID);

                if (itemToEdit != null)
                {
                    itemToEdit.Quantity = quantity;
                    _db.SaveChanges();
                }
                else
                {
                    Debug.WriteLine($"Error editing Product {productID} in Cart {activeCart.UserCartID}.");
                }
            }
            else
            {
                Debug.WriteLine($"Error: Could not find an active cart for User {userID}.");
            }

        }

        public int IsInCart(int userID, int productID)
        {
            UserCart activeCart = _db.UserCarts.FirstOrDefault(cart => cart.UserID == userID
                                                                && cart.Active == true);
            if (activeCart != null)
            {
                // Return quantity of product, if any, in cart
                return _db.CartProducts.Count(item => item.UserCartID == activeCart.UserCartID && item.ProductID == productID);
            }
            else
            {
                throw new Exception($"Error: Could not find an active cart for User {userID}.");
            }
        }

        public void Add()
        {
            // Create new UserCart
        }
    }
}