using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class UserModel
    {
        [Display(Name = "Brukernavn(Epost):")]
        [Required(ErrorMessage="Du må oppgi epostadresse")]
        public string username { get; set; }

        [Display(Name = "Passord:")]
        [Required(ErrorMessage = "Du må oppgi passord")]
        public string password { get; set; }

    }
}