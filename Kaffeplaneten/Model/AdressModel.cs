using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class AdressModel
    {
        public int personID { get; set; }
        public int adressID { get; set; }
        public bool payAdress { get; set; }
        public bool deliveryAdress { get; set; }

        [Display(Name = "Postnummer:")]
        [Required(ErrorMessage = "Postnummer må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnummeret må være 4 siffer langt")]
        public string zipCode { get; set; }

        [Display(Name = "Poststed:")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        public string province { get; set; }

        [Display(Name = "Adresse:")]
        [Required(ErrorMessage = "Adresse må oppgis")]
        public string streetName { get; set; }
    }
}