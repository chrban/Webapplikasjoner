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

        //TODO - Christer: Skal endres (return hele ProductsDBlist)
        public List<Products> getProductsByCategory(string kategori)
        {
            var db = new CustomerContext();

            var produkter = db.Products.Where(s => s.category == kategori).ToList();

            Debug.WriteLine("LIST metode " + produkter);
            return produkter;
        }

        //Brukes ikke
        public List<Products> henteProdukter()
        {
            var db = new CustomerContext();

            var produkter = db.Products.Where(s => s.productID == 1);

            return produkter.ToList();
        }
    }
}