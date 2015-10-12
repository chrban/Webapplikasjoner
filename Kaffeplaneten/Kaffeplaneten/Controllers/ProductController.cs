using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaffeplaneten.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            RedirectToAction("AllProductsView");

            return View();
        }

        public ActionResult AllProducts()
        {

            if (ModelState.IsValid)
            {
                
                var productDB = new DBProduct();

 
                ViewData.Model = productDB.MakeList();
              
            }
            return View();


        }



    }
}

