using Kaffeplaneten.BLL;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
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
            var utListe = new List<ProductModel>();

            return View(utListe);
        }







    }
}