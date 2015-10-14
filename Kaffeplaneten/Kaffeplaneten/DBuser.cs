using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaffeplaneten
{
    public class DBuser
    {

        public bool add(byte[] IncPassword, Customers newCustomer, CustomerContext db)
        {
            try
            {
                Debug.WriteLine("Test1,5");
                Debug.WriteLine("Test2");
                var newUser = new Users()
                {
                    email = newCustomer.email,
                    password = IncPassword
                };
                newUser.customer = newCustomer;
                db.Users.Add(newUser);
                Debug.WriteLine("Lagring fullført!");
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                return false;

            }
        }
    }
}
