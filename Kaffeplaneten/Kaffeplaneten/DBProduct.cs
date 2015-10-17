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

        public List<ProductModel> getAllProducts()
        {
            var db = new CustomerContext();
            List<ProductModel> ProductList = new List<ProductModel>();

            var produkter = (from p in db.Products select p).ToList();
            foreach (var p in produkter)
            {
                var newProductModel = new ProductModel();
                newProductModel.productID = p.productID;
                newProductModel.category = p.category;
                newProductModel.productName = p.productName;
                newProductModel.price = p.price;
                newProductModel.imageURL = p.imageURL;
                newProductModel.description = p.description;
                newProductModel.stock = p.stock;
                ProductList.Add(newProductModel);
                
            }


            return ProductList;
        }

     /*   public List<Products> getAllCategories()
        {
            var db = new CustomerContext();

            var catergories =(from c in db.Products
                              select)
                
                
                //(p => p.category).Distinct().ToList();

           // foreach (var p in db.Products.Select(p => p.category).Distinct().Select(p));
            //    return catergories;
        }
        */
        
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