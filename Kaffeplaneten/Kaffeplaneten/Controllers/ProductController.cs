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

        public ActionResult GetAllProducts()
        {
            var productDB = new DBProduct();

            var ProduktList = productDB.getAllProducts();

            Session["ProductList"] = ProduktList;

            return View(ProduktList);
        }
        public ActionResult ProductDetails(int id)
        {

            var ProduktList = (List<ProductModel>)Session["ProductList"];

            Debug.WriteLine("Id i Detailcontroller" + id);
            foreach(var product in ProduktList)
            {
                if (product.productID == id)
                {
                    return View(product);
                }
            }


            return View();
        }

        /*
                [HttpPost]
                public JsonResult GetProductById(int id)
                {
                    Debug.WriteLine("getProduct metoden kjører ----------- motatt id: " + id);

                    var listen = (List<Products>)Session["list"];
                    listen.ToList();
                    foreach(var i in listen)
                    {
                        if (i.productID == id )
                        {
                            Session["ProduktID"] = id;

                            return Json(i, JsonRequestBehavior.AllowGet);
                        }

                    }
                            return Json("Error", JsonRequestBehavior.AllowGet);
                }

                public void setSessionId(int id)
                {
                    Debug.WriteLine("SetSessionID = " + id);
                    Session["ProduktID"] = id;
                }

                    */




        /* public JsonResult GetUniqeCategories()
         {
             var productDb = new DBProduct();

             var listen = productDb.getAllCategories();

             foreach (var b in listen)
             {
                 if (!nedtrekk.Contains(b.Sjanger))
                 {
                     nedtrekk.Add(b.Sjanger);
                 }
             }

             Debug.Write("liste " + listen);

             JsonResult ut = Json(listen, JsonRequestBehavior.AllowGet);
             return ut;
         }
         */




        //TODO - Christer: Endre metode til å holde Jsonobjektet alive for å sortere på klientside
        public JsonResult hentProdukter(String kategori)
        {
            var productDB = new DBProduct();

            JsonResult ut = Json(productDB.getProductsByCategory(kategori), JsonRequestBehavior.AllowGet);
            return ut;
            
        }
    }
}

