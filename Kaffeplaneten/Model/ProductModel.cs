using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class ProductModel
    {

        [Display(Name = "#")]
        public int productID { get; set; }
        public int quantity { get; set; }
        public string imageURL { get; set; }

        [Display(Name = "Produktnavn:")]
        public string productName { get; set; }
        [Display(Name = "Varelager:")]
        public int stock { get; set; }

        [Display(Name = "Pris: ")]
        public double price { get; set; }

        [Display(Name = "Kategori")]
        public string category { get; set; }

        [Display(Name = "Beskrivelse:")]
        public string description { get; set; }
    }
}