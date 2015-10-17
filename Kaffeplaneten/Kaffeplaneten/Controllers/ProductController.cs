using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//TODO: Legge til feilhåndtering. Hvorfor fungerer ikke: using(var productBD = new DBProduct() ) ??
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

        //public void FilterFroducts(List<ProductModel> ProductList)

        public ActionResult ProductCategories()
        {
            var uniqeCategories = new List<string>();
            var ProductList = (List<ProductModel>)Session["ProductList"];
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
            Session["UniqueCategories"] = uniqeCategories;
            return View(uniqeCategories);
        }

        public PartialViewResult ProductsInCategory(string category)
        {
            var ProductList = (List<ProductModel>)Session["ProductList"];
            var utListe = new List<ProductModel>();
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

            Session["ProductList"] = ProductList;
            return View(ProductList);
        }
        public ActionResult ProductDetails(int id)
        {
            var ProduktList = (List<ProductModel>)Session["ProductList"];
            foreach(var product in ProduktList)
            {
                if (product.productID == id)
                {
                    return View(product);
                }
            }
            return View();
        }


        public void addToCart(ProductModel productModel)
        {
            var orderModel = (OrderModel)Session[SHOPPING_CART];
            if (orderModel == null)
                orderModel = new OrderModel();
            foreach(var p in orderModel.products)
                if(p.productID == productModel.productID)
                {
                    p.quantity += productModel.quantity;
                    return;
                }
            orderModel.products.Add(productModel);
            Session[SHOPPING_CART] = orderModel;
        }
    }
}

