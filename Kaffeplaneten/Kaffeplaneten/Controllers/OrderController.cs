using System.Web.Mvc;
using Kaffeplaneten;
using Kaffeplaneten.Models;
using System.Diagnostics;
using System.Collections.Generic;

namespace Kaffeplaneten.Controllers
{
    public class OrderController : SuperController
    {
        // GET: Order
        public ActionResult confirmOrderView()
        {
            var orderModel = (OrderModel)Session[SHOPPING_CART];
            if (orderModel == null)
                return RedirectToAction("AllProducts", "Product", new { area = "" });
            var customerModel = DBCustomer.find(getActiveUserID());
            if (customerModel == null)
            {
                ModelState.AddModelError("", "Du må være logget inn for å fortsette");
                return RedirectToAction("Loginview", "Security", new { area = "" });
            }
            ViewBag.customerModel = customerModel;
            orderModel.customerID = customerModel.customerID;
            return View(orderModel);
        }

        /*public ActionResult createOrder()                                                       // Creates the order.
        {
            if (getShoppingCart().amountOfItems() > 0)                                           // ----- ALSO NEEDS TO CHECK FOR LOGGED IN SESSION!!! ----
            {
                Debug.WriteLine("Shopping Cart has items. Making Order...");
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("Order is valid.");
                    List<ProductOrders> listOfProducts = new List<ProductOrders>();             // Converts Shopping Cart into ProductOrders. These are not complete
                    foreach (var cartItem in getShoppingCart().ItemsInShoppingCart)              // But will be completed inside the DBOrder method for adding OrderNr and such.
                    {
                        for (int i = 0; i < cartItem.Quanitity; i++)
                        {
                            var newProduct = new ProductOrders();
                            newProduct.price = cartItem.product.price;
                            newProduct.productID = cartItem.product.productID;
                            newProduct.products = cartItem.product;
                            newProduct.quantity = cartItem.Quanitity;
                            listOfProducts.Add(newProduct);
                        }
                    }

                    // ----------- FJERN DETTE NÅR SESSION ER I BRUK -----------
                    Customers customer = new Customers();
                    customer.customerID = 125125;
                    customer.firstName = "Ola";
                    customer.lastName = "Nordmann";
                    customer.email = "hei@hei.hei";
                    customer.phone = "12345678";
                    // ----------- --------------------------------- -----------

                    var db = new CustomerContext();
                    var orderDB = new DBOrder();
                    var insertOK = orderDB.add(listOfProducts, customer, db);
                    if (insertOK)
                    {
                        db.SaveChanges();
                        return RedirectToAction("OrderView");       // ------- SKAL BLI RECEIPTVIEW NÅR DET ER LAGET! ------
                    }
                }
             }
            return View();
        } */// END OF METHOD: CREATEORDER

        public ActionResult orderHistoryView()
        {
            var order = DBOrder.findOrders(getActiveUserID());
            if(order == null)
                return RedirectToAction("Loginview", "Security", new { area = "" });
            return View(order);
        }
        public ActionResult receiptView()
        {
            var orderModel = (OrderModel)Session[SHOPPING_CART];
            if (orderModel == null)
            {
                ModelState.AddModelError("", "Orderen er allerede registrert");
                return View(new OrderModel());
            }
            if(!saveOrder(orderModel))
            {
                ModelState.AddModelError("", "Feil ved registrering av data");
                return View(new OrderModel());
            }
            return View(orderModel);
        }
        public bool saveOrder(OrderModel orderModel)
        {
            if (orderModel == null)
                return false;
            Session[SHOPPING_CART] = null;
            return DBOrder.add(orderModel);
        }
    }
}