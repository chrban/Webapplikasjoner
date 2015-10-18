using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//TODO: Legge til feilhåndtering. Hvorfor fungerer ikke: using(var productBD = new DBProduct() ) ??
//Sette const variabler til sessions
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
      

        public ActionResult ProductCategories()
        {
            var uniqeCategories = new List<string>();
            var ProductList = (List<ProductModel>)Session[PRODUCT_LIST];
            if (ProductList == null)
                    {
                        try
                        {
                            var productDB = new DBProduct();
                            ProductList = productDB.getAllProducts();
                            if (ProductList == null)
                                throw new Exception();
                        }
                        catch(Exception NPF)
                        {
                            return PartialView("No products found.. " + NPF.Message); // riktig å gi denne til bruker?
                        }
                    }
                    foreach (var c in ProductList)
                    {
                        if (!uniqeCategories.Contains(c.category))
                            uniqeCategories.Add(c.category);
                    }
            Session[UNIQUE_CATEGORIES] = uniqeCategories;
            return View(uniqeCategories);
        }

        public PartialViewResult ProductsInCategory(string category)
        {
            var ProductList = (List<ProductModel>)Session[PRODUCT_LIST];
            var utListe = new List<ProductModel>();

            if (category == "ALL")//endre konst
            {
                return PartialView(ProductList);
            }


            foreach(var product in ProductList)
            {
                if (product.category == category)
                    utListe.Add(product);
            }
            return PartialView(utListe);
        }


        public ActionResult GetAllProducts()
        {
            var productDB = new DBProduct();
            var ProductList = productDB.getAllProducts();


            Session[PRODUCT_LIST] = ProductList;
            return View(ProductList);
        }
        public ActionResult ProductDetails(int id)
        {
            var ProduktList = (List<ProductModel>)Session[PRODUCT_LIST];
            foreach(var product in ProduktList)
            {
                if (product.productID == id)
                {
                    return View(product);
                }
            }
            return View();
        }
    }
}

