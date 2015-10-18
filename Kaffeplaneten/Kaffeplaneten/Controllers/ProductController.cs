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



            //GetAllProducts();
            ProductsInCategory(INITIAL_LOAD);

            return View();
        }
      
        //ProductCategories
        public ActionResult ProductCategories()
        {


            return PartialView("ProductCategories");
        }

        public void UniqueCategory()
        {
            var uniqeCategories = new List<string>();
            var ProductList = (List<ProductModel>)Session[PRODUCT_LIST];
            if (ProductList == null)
                ProductList = DBProduct.getAllProducts();
                    foreach (var c in ProductList)
                    {
                        if (!uniqeCategories.Contains(c.category))
                            uniqeCategories.Add(c.category);
                    }
            Session[UNIQUE_CATEGORIES] = uniqeCategories;

        }


        public PartialViewResult ProductsInCategory(string category)
        {
            var productDB = new DBProduct();
            var ProductList = productDB.getAllProducts();



            Session[PRODUCT_LIST] = ProductList;


            //var ProductList = (List<ProductModel>)Session[PRODUCT_LIST];
            var utListe = new List<ProductModel>();

            Debug.WriteLine("Før if, categorystring er: " + category);

            if (category == INITIAL_LOAD)//endre konst
            {
                Debug.WriteLine("INNE ALLE IF-----");
                return PartialView(ProductList);
            }
            else
            {
                Debug.WriteLine("Inni else i category");

                foreach (var product in ProductList)
            {
                if (product.category == category)
                    utListe.Add(product);
            }
            return PartialView(utListe);
        }


        }


        public ActionResult GetAllProducts()
        {
            var ProductList = DBProduct.getAllProducts();


            Session[PRODUCT_LIST] = ProductList;
            return View(ProductList);
        }
        public ActionResult ProductDetails(int id)
        {
            var ProduktList = (List<ProductModel>)Session[PRODUCT_LIST];
            if (ProduktList == null)
                return View();
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

