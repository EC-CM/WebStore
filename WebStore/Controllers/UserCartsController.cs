using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

        public ActionResult Cart(int userID)
        {
            UserCart activeCart = _db.UserCarts.FirstOrDefault(cart => cart.UserID == userID
                                                    && cart.Active == true);
            if (activeCart != null)
            {
                ViewModel models = new ViewModel
                {
                    CartProducts = _db.CartProducts
                        .Where(p => p.UserCartID == activeCart.UserCartID)
                        .Include(p => p.Product)
                        .ToList()
                };

                /*
                models.Products = _db.Products
                        .Where(product => models.CartProducts
                            .Any(cartProduct => cartProduct.ProductID == product.ProductID))
                        .ToList();
                */

                return View("Cart", models);
            }
            else
            {
                Debug.WriteLine($"Error: Could not find an active cart for User {userID}.");
                return Content("404: Cart not found.");
            }  
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

        public ActionResult RemoveFromCart(int userID=0, int productID=0, int cartProductID=0)
        {
            if (cartProductID != 0)
            {
                Debug.WriteLine($"Deleted cart item {cartProductID}.");
                CartProduct itemToRemove = _db.CartProducts.FirstOrDefault(item => item.CartProductID == cartProductID);
                _db.CartProducts.Remove(itemToRemove);
                _db.SaveChanges();
            }
            else
            {
                Debug.WriteLine($"Deleted Product {productID} from User {userID}\'s cart.");
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

            return RedirectToAction("/Cart", new { userID = 1 });

        }

        public void EditCartItem(int cartProductID, int quantity)
        {            
                _db.CartProducts.FirstOrDefault(cartProduct => cartProduct.CartProductID == cartProductID)
                    .Quantity = quantity;
                _db.SaveChanges();              
        }

        public void OldEditCartItem (int userID, int productID, int quantity)
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

        public int TotalItemsInCart(int userID)
        {
            UserCart activeCart = _db.UserCarts.FirstOrDefault(cart => cart.UserID == userID
                                                                && cart.Active == true);
            if (activeCart != null)
            {
                int cartTotal = 0;

                foreach (CartProduct item in _db.CartProducts
                    .Where(i => i.UserCartID == activeCart.UserCartID)
                    .ToList())
                {
                    cartTotal += item.Quantity;
                }
                
                return cartTotal;

                /*return _db.CartProducts
                        .Where(i => i.UserCartID == activeCart.UserCartID)
                        .Count();
                */
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

        // Manage function name not matching view name (when running via view in IIS Express).
        //public ActionResult Cart() { return RedirectToAction("Index", 1); }
    }
}