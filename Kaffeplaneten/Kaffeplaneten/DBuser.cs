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

        public bool add(byte[] IncPassword, Customers newCustomer)
        {
            try
            {
                Debug.WriteLine("Test1,5");
                var db = new CustomerContext();
                Debug.WriteLine("Test2");
                var newUser = new Users()
                {
                    email = newCustomer.email,
                    password = IncPassword
                };
                newUser.customer = newCustomer;
                //db.Users.Find(newUser);
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
