using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class Customer : Person
    {

        public int customerID { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Postnummer må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnummeret må være 4 siffer")]
        public string zipCode { get; set; }

        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        public string province { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adresse må oppgis")]  
        public string adress { get; set; }

        [Display(Name = "Betalingsadresse")]
        [Required(ErrorMessage = "Betalingsadresse må oppgis")]
        public string payAdress { get; set; }

        [Display(Name = "Bet. Postnummer")]
        [Required(ErrorMessage = "Betalingspostnummer må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnummeret må være 4 siffer")]
        public string payZipcode { get; set; }

        [Display(Name = "Bet. Poststed")]
        [Required(ErrorMessage = "Betalingspoststed må oppgis")]
        public string payProvince { get; set; }
       



    }
}