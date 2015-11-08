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
        private LoggingBLL _loggingBLL;

        public AdminProductController()
        {
            _productBLL = new ProductBLL();
            _loggingBLL = new LoggingBLL();
        }
        public AdminProductController(ProductBLL productBLL, LoggingBLL logginBLL)
        {
            _productBLL = productBLL;
            _loggingBLL = logginBLL;
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
            var productModel = _productBLL.find(id);
            if (productModel == null)
                return View();
            UniqueCategory();
            Session["tempPID"] = id;
                    
            return View(productModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            productModel.productID = (int)Session["tempPID"];

            if (_productBLL.update(productModel))
            {
                _loggingBLL.logToUser("Oppdaterte produkt: " + productModel.productName, (EmployeeModel)Session["Employee"]);
                _loggingBLL.logToDatabase("Produkt " + productModel.productName + " ble oppdatert.");
                return RedirectToAction("AllProducts");
            }
            else 
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
             if (_productBLL.delete(id))
             {
                _loggingBLL.logToUser("Slettet produkt med ProduktID: " + id, (EmployeeModel)Session["Employee"]);
                _loggingBLL.logToDatabase("Produkt med ID:" + id + " ble slettet fra databasen.");
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
                _loggingBLL.logToUser("La til produkt '" + newProduct.productName + "' i databasen.", (EmployeeModel)Session["Employee"]);
                _loggingBLL.logToDatabase("Produkt " + " ble lagt til i databasen.");
                return RedirectToAction("AllProducts");

            }
            else
            {
                return View();

            }

        }

        public ActionResult Details(int id)
        {
            var productModel = _productBLL.find(id);
            if(productModel==null)
                return RedirectToAction("AllProducts");
            return View(productModel);
        }


        private void UniqueCategory()
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