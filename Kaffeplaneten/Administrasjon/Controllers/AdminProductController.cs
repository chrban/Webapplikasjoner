using Kaffeplaneten.BLL;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administrasjon.Controllers
{
    public class AdminProductController : Controller
    {

        private ProductBLL _productBLL;

        public AdminProductController()
        {
            _productBLL = new ProductBLL();
        }

        public ActionResult AllProducts()
        {
            var ProductList = _productBLL.getAllProducts();

            if(ProductList!=null)
            {
                return View(ProductList);

            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            var ProductList = _productBLL.getAllProducts();
            foreach(var i in ProductList)
            {
                if(i.productID==id)
                {
                    UniqueCategory();
                    Session["tempPID"] = id;
                    
                    return View(i);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            productModel.productID = (Int32)Session["tempPID"];

            if (_productBLL.update(productModel))
            {
                return RedirectToAction("AllProducts");
            }
            else 
            {
                Debug.WriteLine("EDIT FEILER");
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
             if (_productBLL.delete(id))
             {
                 return RedirectToAction("AllProducts");
             }
             else
             {
                 return View();
             }
        }


        public ActionResult Add()
        {
            UniqueCategory();
            return View("Add");
        }

        [HttpPost]
        public ActionResult Add(ProductModel newProduct)
        {

            if (_productBLL.add(newProduct))
            {
                return RedirectToAction("AllProducts");

            }
            else
            {
                return View();

            }
            

        }

        public ActionResult Details(int id)
        {
            var ProductList = _productBLL.getAllProducts();
            foreach (var i in ProductList)
            {
                if (i.productID == id)
                {
                    return View(i);
                }
            }

            return RedirectToAction("AllProducts");


        }


        public void UniqueCategory()
        {
            var uniqeCategories = new List<String>();
            var ProductList = _productBLL.getAllProducts();

            foreach (var c in ProductList)
            {
                if (!uniqeCategories.Contains(c.category))
                {
                    uniqeCategories.Add(c.category);
                }
            }
            ViewBag.uniqeCategories = uniqeCategories;
        }
        
        public ActionResult Uploader()
        {
            return View();
        }

    }
}