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


       /* public List<Products> MakeList()
        {
            var db = new CustomerContext();
            List<Products> listOfAllProducts = db.Products.ToList();

            return listOfAllProducts;

        }
        */


        public List<ProductModel> MakeList()
        {
            var db = new CustomerContext();
            List<Products> liste = db.Products.ToList();
            List<ProductModel> utListe = new List<ProductModel>();

            foreach(var i in liste)
            {

                utListe.Add(toObject(i));
            }
            return utListe;
        }

        public Products getProduct(int pID)
        {
            var db = new CustomerContext();
            var product = db.Products.FirstOrDefault(p => p.productID == pID);

            if(product == null)
            {
                //fant ikke produkt
            }
            return product;

        }

       
        public static ProductModel toObject(Products inProd)
        {
            var db = new CustomerContext();
            var i = db.Products.Find(inProd.productID);

            var newProduct = new ProductModel();
            newProduct.productID = i.productID;
            newProduct.productName = i.productName;
            newProduct.price = i.price;
            newProduct.imageURL = i.imageURL;
            newProduct.description = i.description;
            newProduct.category = i.category;
            newProduct.stock = i.stock;
            return newProduct;

        }


    }






    
   
}