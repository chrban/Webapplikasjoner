using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaffeplaneten.Controllers
{
    public class ProductController : SuperController
    {
        // GET: Product
        public ActionResult Index()
        {
           
            RedirectToAction("AllProductsView");
            

            return View();
        }

        public ActionResult AllProducts()
        {
            GetAllProducts();
            return View();
        }

        public JsonResult GetAllProducts()
        {
            var productDB = new DBProduct();

            Debug.WriteLine("json metoden kjører");
            JsonResult ut = Json(productDB.getAllProducts(), JsonRequestBehavior.AllowGet);

            return ut;
        }

        



        //TODO - Christer: Endre metode til å holde Jsonobjektet alive for å sortere på klientside
        public JsonResult hentProdukter(String kategori)
        {
            var productDB = new DBProduct();

            JsonResult ut = Json(productDB.getProductsByCategory(kategori), JsonRequestBehavior.AllowGet);
            return ut;
            
        }
    }
}

