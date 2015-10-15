using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class UserModel
    {
        [Required(ErrorMessage="Du må oppgi brukernavn")]
        public string username { get; set; }
        [Required(ErrorMessage = "Du må oppgi passord")]
        public string password { get; set; }

    }
}