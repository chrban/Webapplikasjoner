using System.Web.Mvc;
using Kaffeplaneten.Models;
using System.Diagnostics;

namespace Kaffeplaneten.Controllers
{
    public class OrderController : SuperController
    {
        // GET: Order
       
        public ActionResult OrderView()                    // Returns the order confirmation view.
        {
            return View();
        }

        public ActionResult createOrder()                                                       // Creates the order.
        {
            ShoppingCartController cartController = new ShoppingCartController();
            if(cartController.amountOfItems() > 0)                                           // ----- ALSO NEEDS TO CHECK FOR LOGGED IN SESSION!!! ----
            {
                Debug.WriteLine("Shopping Cart has items. Making Order...");
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("Order is valid.");

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
                    var insertOK = orderDB.add(customer, db);
                    if (insertOK)
                    {
                        return RedirectToAction("OrderView");       // ------- SKAL BLI RECEIPTVIEW NÅR DET ER LAGET! ------
                    }
                }
             }
            return View();
        } // END OF METHOD: CREATEORDER

    }
}