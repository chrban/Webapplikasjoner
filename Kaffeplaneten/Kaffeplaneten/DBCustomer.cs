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
        public Customers add(CustomerModel IncCustomer,  CustomerContext db)
        {
            Debug.WriteLine("Test1");

            try
            {
                if (db.Customers.Find(IncCustomer.customerID) == null)
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
        public static CustomerModel find(int id)
        {
            var customerModel = new CustomerModel();
            try
            {
                var db = new CustomerContext();
                var temp = (from c in db.Customers
                            where c.customerID == id
                            select c).FirstOrDefault();

                customerModel.customerID = temp.customerID;
                customerModel.firstName = temp.firstName;
                customerModel.lastName = temp.lastName;
                customerModel.email = temp.email;
                customerModel.phone = temp.phone;
                List<Adresses> adresses = (from a in db.Adresses
                                           where a.customerID == customerModel.customerID
                                           select a).ToList();
                foreach (var a in adresses)
                {
                    if (a.deliveryAdress)
                    {
                        customerModel.adress = a.streetName;
                        customerModel.province = a.province.province;
                        customerModel.zipCode = a.zipCode;
                    }
                    if (a.payAdress)
                    {
                        customerModel.payAdress = a.streetName;
                        customerModel.payProvince = a.province.province;
                        customerModel.payZipcode = a.zipCode;
                    }
                }
                return customerModel;
            }

            catch (Exception ex)
            {
                /*Viser nyttig informasjon om alle excetions i debug.out. Avslutter programmet*/
                Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nSource:\n" + ex.Source);
                Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                //Environment.Exit(1);
            }
            return null;
        }

        public static bool update(CustomerModel customerModel)
        {
            try
            {
                var db = new CustomerContext();
                var customer = (from c in db.Customers
                                where c.customerID == customerModel.customerID
                                select c).FirstOrDefault();
                if (customer == null)
                {
                    return false;

                }
                //Persondataendring:
                customer.email = customerModel.email;
                customer.firstName = customerModel.firstName;
                customer.lastName = customerModel.lastName;
                customer.phone = customerModel.phone;

                customer.users.email = customerModel.email;
                db.SaveChanges();

                //Adresseendring:
                db = new CustomerContext();
                var adresses = (from a in db.Adresses
                                where a.customerID == customerModel.customerID
                                select a).ToList();
                db.Dispose();
                bool newPayAdress = true;
                bool newDeliveryAdress = true;
                foreach (var a in adresses)
                {

                    if (a.deliveryAdress && a.streetName.Equals(customerModel.adress) && a.zipCode.Equals(customerModel.zipCode))
                        newDeliveryAdress=false;
                    if (a.payAdress && a.streetName.Equals(customerModel.payAdress) && a.zipCode.Equals(customerModel.payZipcode))
                        newPayAdress=false;

                }

                if (newPayAdress)
                {

                    var adressModel = new AdressModel();
                    adressModel.customerID = customerModel.customerID;
                    adressModel.streetName = customerModel.payAdress;
                    adressModel.province = customerModel.payProvince;
                    adressModel.zipCode = customerModel.payZipcode;
                    adressModel.delivieryAdress = customerModel.sameAdresses;
                    adressModel.payAdress = true;
                }
                if (newDeliveryAdress && !customerModel.sameAdresses)
                {
                    var adressModel = new AdressModel();
                    adressModel.customerID = customerModel.customerID;
                    adressModel.streetName = customerModel.adress;
                    adressModel.province = customerModel.province;
                    adressModel.zipCode = customerModel.zipCode;
                    adressModel.delivieryAdress = true;
                    adressModel.payAdress = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nSource:\n" + ex.Source);
                Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
            }
            return false;
        }

        public static Provinces getProvince(string zipCode)
        {
            try
            {
                var db = new CustomerContext();
                var province = db.Provinces.Find(zipCode);
                return province;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nSource:\n" + ex.Source);
                Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
            }
            return null;
        }

        public static Adresses addAdress(AdressModel adressModel)
        {
            try
            {
                var db = new CustomerContext();
                var adress = new Adresses();
                adress.payAdress = adressModel.payAdress;
                adress.deliveryAdress = adressModel.delivieryAdress;
                adress.streetName = adressModel.streetName;
                adress.customers = db.Customers.Find(adressModel.customerID);
                adress.province = addProvince(adressModel.zipCode, adressModel.province);
                db.Adresses.Add(adress);
                db.SaveChanges();
                return adress;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nSource:\n" + ex.Source);
                Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
            }
            return null;
        }

        public static Provinces addProvince(string zipCode, string province)
        {
            try
            {
                var db = new CustomerContext();
                var temp = (from p in db.Provinces
                            where p.zipCode.Equals(zipCode)
                            select p).FirstOrDefault();
                db.Dispose();
                if (temp == null)
                {

                    var newProvinve = new Provinces();
                    newProvinve.province = province;
                    newProvinve.zipCode = zipCode;
                    db = new CustomerContext();
                    db.Provinces.Add(newProvinve);
                    db.SaveChanges();
                    return newProvinve;
                }
                return temp;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nSource:\n" + ex.Source);
                Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                //Environment.Exit(1);
            }

            return null;
        }

    }
}
