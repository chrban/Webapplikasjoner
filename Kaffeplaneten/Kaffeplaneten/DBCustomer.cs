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

            var newCustomer = new Customers()
            {
                firstName = IncCustomer.firstName,
                lastName = IncCustomer.lastName,
                email = IncCustomer.email,
                phone = IncCustomer.phone,
            };
            var newPayAdress = new Adresses()
            {
                streetName = IncCustomer.payAdress,
                zipCode = IncCustomer.payZipcode,
                payAdress = true,
                deliveryAdress = false
            };
            var newDeliveryAdress = new Adresses()
            {
                streetName = IncCustomer.adress,
                zipCode = IncCustomer.zipCode,
                deliveryAdress = true,
                payAdress = false
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
                    db.Provinces.Add(newProvince);
                    newDeliveryAdress.province = newProvince;
                }
                else
                    newDeliveryAdress.province = db.Provinces.Find(IncCustomer.zipCode);
                    //newCustomer.deliveryAdress.province = db.Provinces.Find(IncCustomer.province);

                ExistingZipcode = db.Provinces.Find(IncCustomer.payZipcode);
                if (ExistingZipcode == null)
                {
                    Debug.WriteLine("test444444444444444444444444444444444");
                    var newProvince = new Provinces()
                    {
                        zipCode = IncCustomer.payZipcode,
                        province = IncCustomer.payProvince
                    };
                    db.Provinces.Add(newProvince);
                    newPayAdress.province = newProvince;
                    //newCustomer.payAdress.province = newProvince;
                }
                else
                    newPayAdress.province = db.Provinces.Find(IncCustomer.payZipcode);

                //newCustomer.payAdress.province = db.Provinces.Find(IncCustomer.payZipcode);

                db.Customers.Add(newCustomer);
                
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
