using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaffeplaneten
{

    public class DBCustomer
    {
        public bool add(Customer IncCustomer)
        {
            var newCustomer = new Customers()
            {
                firstName = IncCustomer.firstName,
                lastName = IncCustomer.lastName,
                adress = IncCustomer.adress,
                zipCode = IncCustomer.zipCode,


            };

            var db = new CustomerContext();
            try
            {
                var ExistingZipcode = db.Provinces.Find(IncCustomer.zipCode);

                if (ExistingZipcode == null)
                {
                    var newProvince = new Provinces()
                    {
                        zipCode = IncCustomer.zipCode,
                        province = IncCustomer.province
                    };
                    newCustomer.provinces = newProvince;
                }
                db.Customers.Add(newCustomer);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }
        }

        /*   
         SLETTE-METODE SOM TILHØRER TØNSAGER SIN DEL   

            public bool delete(string email)

           {
               var db = new CustomerContext();
               try
               {
                   Customer deleteCustomer = db.Customers.Find(mail);
                   db.Customers.Remove(deleteCustomer);
                   db.SaveChanges();
                   return true;
               }
               catch (Exception feil)
               {
                   return false;
               }
           } */
    }
}
