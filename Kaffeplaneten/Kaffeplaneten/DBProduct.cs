using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Kaffeplaneten
{
    public class DBProduct
    {

        public List<Products> MakeList()
        {
            var db = new CustomerContext();
            // List<Products> listOfAllProducts = db.Products.ToList();

            return null; //listOfAllProducts;

        }

    }
}