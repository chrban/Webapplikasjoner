using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
namespace Kaffeplaneten.BLL
{
    public class ProductBLL
    {

        private ProductDAL _productDAL;

        public ProductBLL()
        {
            _productDAL = new ProductDAL();
        }
        
        public  List<ProductModel> getAllProducts()//Henter alle produkter fra databasen og oppretter liste av modelobjekter 
        {
            return _productDAL.getAllProducts();
        }
        public bool add(ProductModel productModel)//Legger et produkt inn i databasen
        {
            return _productDAL.add(productModel);
        }
        public bool updateQuantity(ProductModel productModel)//Oppdaterer lagerstatur på produkt. Bruker productModel.stock som ny verdi
        {
            return _productDAL.updateQuantity(productModel);
        }
        public ProductModel find(int id)//Henter ut produkt med id lik id
        {
            return _productDAL.find(id);
        }
    }
}