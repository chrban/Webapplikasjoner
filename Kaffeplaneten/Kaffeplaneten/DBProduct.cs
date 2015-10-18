using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
// Legge til feilhåndtering: Using, try catch osv
namespace Kaffeplaneten
{
    public class DBProduct
    {
        //Henter alle produkter fra databasen og oppretter liste av modelobjekter 
        public static List<ProductModel> getAllProducts()
        {
            using (var db = new CustomerContext())
            {
                List<ProductModel> ProductList = new List<ProductModel>();
                try
                {

                    ProductList = new List<ProductModel>();

                    var produkter = (from p in db.Products select p).ToList();
                    if(produkter!=null)
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
                catch (Exception error)
                {
                }
                return null;
            }
        }
        public static bool add(ProductModel productModel)
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var product = new Products();
                    product.category = productModel.category;
                    product.description = productModel.description;
                    product.imageURL = productModel.imageURL;
                    product.price = productModel.price;
                    product.productName = productModel.productName;
                    product.stock = productModel.stock;
                    db.Products.Add(product);
                    db.SaveChanges();
                    return true;
                }
                catch(Exception)
                {
                    return false;
                }
            }
        }


        //allt under denne linjen kan slettes for Christer sin del!! ______________________________________________________________________________________




        //TODO - Christer: Skal endres (return hele ProductsDBlist)
        public static List<Products> getProductsByCategory(string kategori)
        {
            var db = new CustomerContext();

            var produkter = db.Products.Where(s => s.category == kategori).ToList();

            Debug.WriteLine("LIST metode " + produkter);
            return produkter;
        }

        //Brukes ikke
        public static List<Products> henteProdukter()
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
                    if (product == null)
                        return null;
                    productModel.productID = product.productID;
                    productModel.productName = product.productName;
                    productModel.price = product.price;
                    productModel.imageURL = product.imageURL;
                    productModel.description = product.description;
                    productModel.category = product.category;
                    productModel.stock = product.stock;
                    return productModel;
                }
                catch (Exception)
                {
                    return null;
                }
            }

        }
    }




}