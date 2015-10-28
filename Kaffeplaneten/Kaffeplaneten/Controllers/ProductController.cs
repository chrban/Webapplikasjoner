using Kaffeplaneten.BLL;
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
        private ProductBLL _productBLL;

        public ProductController()
        {
            _productBLL = new ProductBLL();
        }
        public ActionResult AllProducts()
        {
            ProductsInCategory(INITIAL_LOAD);

            return View();
        }
      
        public ActionResult ProductCategories()
        {
            return PartialView("ProductCategories");
        }

        public void UniqueCategory()
        {
            var uniqeCategories = new List<string>();
            var ProductList = (List<ProductModel>)Session[PRODUCT_LIST];
            if (ProductList == null)
                ProductList = _productBLL.getAllProducts();
                    foreach (var c in ProductList)
                    {
                        if (!uniqeCategories.Contains(c.category))
                            uniqeCategories.Add(c.category);
                    }
            Session[UNIQUE_CATEGORIES] = uniqeCategories;

        }
        public PartialViewResult ProductsInCategory(string category)
        {
            var ProductList = _productBLL.getAllProducts();
            var utListe = new List<ProductModel>();
            Session[PRODUCT_LIST] = ProductList;

            if (category == INITIAL_LOAD)
            {
                return PartialView(ProductList);
            }
            else
            {
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
            var ProductList = _productBLL.getAllProducts();


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

