using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaffeplaneten
{
    public class DBuser
    {

        public bool add(byte[] IncPassword, Customers IncCustomer)
        {
            try
            {
                Debug.WriteLine("Test1,5");
                var db = new CustomerContext();
                Debug.WriteLine("Test2");
                var newUser = new Users()
                {
                    email = IncCustomer.email,
                    password = IncPassword
                };
                newUser.customer = IncCustomer;
                 db.SaveChanges();
                Debug.WriteLine("Lagring fullført!");
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }

        }
    }
}
