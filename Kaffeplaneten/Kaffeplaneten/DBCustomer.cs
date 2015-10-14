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
        public Customers add(CustomerModel IncCustomer)
        {
            Debug.WriteLine("Test1");

            try
            {
                var db = new CustomerContext();
                if (db.Customers.Find(IncCustomer.email) == null)
                {
                    var newCustomer = new Customers()
                    {
                        email = IncCustomer.email,
                        firstName = IncCustomer.firstName,
                        lastName = IncCustomer.lastName,
                        phone = IncCustomer.phone
                    };
                    Debug.WriteLine("Test2");
                    //Sjekker om adressene er like
                    if (IncCustomer.payAdress.Equals(IncCustomer.adress))
                    {
                        var newAdress = new Adresses()
                        {
                            payAdress = true,
                            deliveryAdress = true,
                            zipCode = IncCustomer.zipCode,
                            streetName = IncCustomer.adress,
                        };
                        newAdress.customers = newCustomer;
                        Debug.WriteLine("Test3");
                        Debug.WriteLine("Test4");
                        var ExistingProvince = db.Provinces.Find(IncCustomer.payZipcode);
                        Debug.WriteLine("test5");

                        if (ExistingProvince == null)
                        {
                            var newProvince = new Provinces()
                            {
                                zipCode = IncCustomer.zipCode,
                                province = IncCustomer.province
                            };
                            newAdress.province = newProvince;
                            db.Provinces.Add(newProvince);
                        }
                        else
                            newAdress.province = ExistingProvince;

                        db.Adresses.Add(newAdress);

                        db.SaveChanges();
                        Debug.WriteLine("SAVEDCHANGED CONFIRMED");
                    }
                    //Ulike adresser
                    else
                    {
                        var newPaymentAdress = new Adresses()
                        {
                            payAdress = true,
                            deliveryAdress = false,
                            zipCode = IncCustomer.payZipcode,
                            streetName = IncCustomer.payAdress,

                        };
                        newPaymentAdress.customers = newCustomer;
                        var ExistingProvince = db.Provinces.Find(IncCustomer.payZipcode);
                        if (ExistingProvince == null)
                        {
                            var newPaymentProvince = new Provinces()
                            {
                                zipCode = IncCustomer.payZipcode,
                                province = IncCustomer.payProvince
                            };
                            newPaymentAdress.province = newPaymentProvince;
                            db.Provinces.Add(newPaymentProvince);
                        }
                        else
                            newPaymentAdress.province = ExistingProvince;
                        db.Adresses.Add(newPaymentAdress);

                        var newAdress = new Adresses()
                        {
                            payAdress = false,
                            deliveryAdress = true,
                            zipCode = IncCustomer.zipCode,
                            streetName = IncCustomer.adress,

                        };
                        ExistingProvince = db.Provinces.Find(IncCustomer.zipCode);
                        if (ExistingProvince == null)
                        {
                            var newProvince = new Provinces()
                            {
                                zipCode = IncCustomer.zipCode,
                                province = IncCustomer.province
                            };
                            newAdress.province = newProvince;
                            db.Provinces.Add(newProvince);
                        }

                        else
                            newAdress.province = ExistingProvince;
                        db.Adresses.Add(newAdress);

                        db.Customers.Add(newCustomer);

                        Debug.WriteLine("Test7");
                        db.SaveChanges();
                        Debug.WriteLine("Test8");
                    }
                    return newCustomer;
                }
                return null;
                
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
                return null;
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
}
