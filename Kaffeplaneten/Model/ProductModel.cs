using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class ProductModel
    {

        [Display(Name = "#ID")]
        public int productID { get; set; }
        public int quantity { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Bildefil:")]
        public string imageURL { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [Display(Name = "Produktnavn:")]
        public string productName { get; set; }

        [Required]
        [Display(Name = "Varelager:")]
        public int stock { get; set; }

        [Required]
        [Display(Name = "Pris: ")]
        [DataType(DataType.Currency)]
        public double price { get; set; }

        [Required]
        [Display(Name = "Kategori")]
        public string category { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "Beskrivelse:")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
    }
}