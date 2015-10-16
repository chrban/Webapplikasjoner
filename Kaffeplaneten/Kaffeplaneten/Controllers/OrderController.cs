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
        public ActionResult OrderView()                    // Returns the order confirmation view.
        {
            return View();
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
    }
}