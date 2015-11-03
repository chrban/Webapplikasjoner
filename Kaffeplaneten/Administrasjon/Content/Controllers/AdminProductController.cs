using Kaffeplaneten.BLL;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            return View(ProductList[id]);
        }

        [HttpPost]
        public ActionResult Edit(ProductModel _productModel)
        {
            Debug.WriteLine("Selve objektet: "+ _productModel);
            Debug.WriteLine("objektet navn: " + _productModel.productName);
            Debug.WriteLine("Inni edit i controller: " + _productModel.productID);
            if(_productBLL.update(_productModel))
            {
                Debug.WriteLine("INNI edit i iffen ");
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

    }
}