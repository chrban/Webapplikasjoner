using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Kaffeplaneten
{

    public class DBCustomer
    {
        public bool add(Customer IncCustomer)
        {
            Debug.WriteLine("Test1");
            var newPerson = new Persons()
            {
                firstName = IncCustomer.firstName,
                lastName = IncCustomer.lastName,
                email = IncCustomer.email,
                phone = IncCustomer.phone
            };

            var newCustomer = new Customers()
            {
                adress = IncCustomer.adress,
                zipCode = IncCustomer.zipCode,
                payAdress = IncCustomer.payAdress,
                payZipCode = IncCustomer.payZipcode,
                payProvince = IncCustomer.payProvince

            };

            
            Debug.WriteLine("Test2");
            var db = new CustomerContext();
            try
            {
                
                var ExistingZipcode = db.Provinces.Find(IncCustomer.zipCode);
                if (ExistingZipcode == null)
                {
                    Debug.WriteLine("test444444444444444444444444444444444");
                    var newProvince = new Provinces()
                    {
                        zipCode = IncCustomer.zipCode, 
                        province = IncCustomer.province
                    };
                    newCustomer.provinces = newProvince;
                    db.Provinces.Add(newProvince);
                }
                else
                    newCustomer.provinces = db.Provinces.Find(IncCustomer.province); 

                //Sjekker om betalingsadressen også eksisterer
                ExistingZipcode = db.Provinces.Find(IncCustomer.payZipcode);
                if (ExistingZipcode == null)
                {
                    Debug.WriteLine("Test5");
                    var newProvince = new Provinces()
                    {
                        zipCode = IncCustomer.payZipcode,
                        province = IncCustomer.payProvince
                    };
                    newCustomer.payProvince = newProvince.province.ToString();
                    Debug.WriteLine(newCustomer.payProvince);
                    db.Provinces.Add(newProvince);
                }
                else
                    newCustomer.payProvince = IncCustomer.payProvince;

                db.Customers.Add(newCustomer);
                db.Persons.Add(newPerson);
                Debug.WriteLine("Test7");
                db.SaveChanges();
                Debug.WriteLine("Test8");
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
