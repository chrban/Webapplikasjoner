using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                var liste = productDB.MakeList();
                
                return View(liste.ToList());
            }
            return View();

        }

 
        public ActionResult ProductDetailsView(int id = 0)
        {
            var productDB = new DBProduct();
            var funnetProd = productDB.getProduct(id);
            if(funnetProd == null)
            {
                return HttpNotFound();
            }
            var ut = productDB.toObject(funnetProd);
            return View(ut);
        }
       


    }
}

