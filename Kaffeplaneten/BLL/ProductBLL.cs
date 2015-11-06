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

        private IProductDAL _productDAL;
        private LoggingBLL _loggingBLL;
        public ProductBLL(IProductDAL iProductDAL)
        {
            _productDAL = iProductDAL;
            _loggingBLL = new LoggingBLL();
        }
        public ProductBLL()
        {
            _productDAL = new ProductDAL();
            _loggingBLL = new LoggingBLL();
        }
        
        public  List<ProductModel> getAllProducts()//Henter alle produkter fra databasen og oppretter liste av modelobjekter 
        {
            return _productDAL.getAllProducts();
        }
        public bool add(ProductModel productModel)//Legger et produkt inn i databasen
        {
            _loggingBLL.logToUser("La til produkt '" + productModel.productName + "' i databasen.");
            _loggingBLL.logToDatabase("Produkt " +  " ble lagt til i databasen.");
            return _productDAL.add(productModel);
        }
        public bool updateQuantity(ProductModel productModel)//Oppdaterer lagerstatur på produkt. Bruker productModel.stock som ny verdi
        {
            _loggingBLL.logToUser("Oppdaterte lagerstatus på " + productModel.productName);
            _loggingBLL.logToDatabase("Produkt " + productModel.productName + " fikk lagerstatus oppdatert.");
            return _productDAL.updateQuantity(productModel);
        }
        public ProductModel find(int id)//Henter ut produkt med id lik id
        {
            return _productDAL.find(id);
        }

        public bool update(ProductModel productModel)
        {
            _loggingBLL.logToUser("Oppdaterte produkt: " + productModel.productName);
            _loggingBLL.logToDatabase("Produkt " + productModel.productName + " ble oppdatert.");
            return _productDAL.update(productModel);
        }
        public bool delete(int id)
        {
            _loggingBLL.logToUser("Slettet produkt med ProduktID: " + id);
            _loggingBLL.logToDatabase("Produkt med ID:" + id + " ble slettet fra databasen.");
            return _productDAL.Delete(id);
        }


    }
}