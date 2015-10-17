using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kaffeplaneten;
using System.Web.Mvc;

namespace Kaffeplaneten.Models
{
    public class ShoppingCartModel
    {
        public List<JsonResult> ItemsInCart { get; set; }
        public int Quanitity { get; set; }
    }
}