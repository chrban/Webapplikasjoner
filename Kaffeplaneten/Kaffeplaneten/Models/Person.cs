using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class Person
    {

        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        public string firstName { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        public string lastName { get; set; }

        [Display(Name = "Epost")]
        [Required(ErrorMessage = "Epost må oppgis")]
        public string email { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Telefon må oppgis")]
        public string phone { get; set; }


    }
}