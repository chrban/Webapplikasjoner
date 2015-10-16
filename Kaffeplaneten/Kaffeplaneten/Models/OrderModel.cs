using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class OrderModel
    {

        public int orderNr { get; set; }
        public int customerID { get; set; }
        public List<ProductModel> products { get; set; }//liste over alle produkter i orderen. Legg inn dublikater hvis flere av samme produkt er bestilt.
        public double total { get; set; }//total pris for orderen

        public OrderModel()
        {
            products = new List<ProductModel>();
        }
    }
}