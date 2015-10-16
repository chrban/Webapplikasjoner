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

        public static ProductModel find(int id)
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var product = db.Products.Find(id);
                    var productModel = new ProductModel();
                    productModel.productID = product.productID;
                    productModel.productName = product.productName;
                    productModel.price = product.price;
                    productModel.imageURL = product.imageURL;
                    productModel.description = product.description;
                    productModel.category = product.category;
                    productModel.stock = product.stock;
                    return productModel;
                }
                catch(Exception)
                {
                    return null;
                }
            }

        }
    }
}