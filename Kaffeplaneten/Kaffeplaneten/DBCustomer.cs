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
        public static bool add(CustomerModel IncCustomer)
        {
            Debug.WriteLine("Test1");
            using (var db = new CustomerContext())
            {
                try
                {
                    if (!(db.Customers.Find(IncCustomer.customerID) == null))
                        return false;
                    var newCustomer = new Customers()
                    {
                        email = IncCustomer.email,
                        firstName = IncCustomer.firstName,
                        lastName = IncCustomer.lastName,
                        phone = IncCustomer.phone
                    };
                    newCustomer = db.Customers.Add(newCustomer);
                    db.SaveChanges();
                    IncCustomer.customerID = newCustomer.customerID;
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
                    }//end foreach
                    return false;
                }//end catch
            }//end using

            Debug.WriteLine(IncCustomer.customerID);

            //Sjekker om adressene er like
            if (IncCustomer.payAdress.Equals(IncCustomer.adress))
            {
                var adressModel = new AdressModel()
                {
                    customerID = IncCustomer.customerID,
                    payAdress = true,
                    deliveryAdress = true,
                    zipCode = IncCustomer.zipCode,
                    streetName = IncCustomer.adress,
                    province = IncCustomer.province
                };


                addAdress(adressModel);

                Debug.WriteLine("SAVEDCHANGED CONFIRMED");
            }
            //Ulike adresser
            else
            {
                var newPaymentAdress = new AdressModel()
                {
                    customerID = IncCustomer.customerID,
                    payAdress = true,
                    deliveryAdress = false,
                    zipCode = IncCustomer.payZipcode,
                    streetName = IncCustomer.payAdress,
                    province = IncCustomer.payProvince

                };
                addAdress(newPaymentAdress);

                var newAdress = new AdressModel()
                {
                    customerID = IncCustomer.customerID,
                    payAdress = false,
                    deliveryAdress = true,
                    zipCode = IncCustomer.zipCode,
                    streetName = IncCustomer.adress,
                    province = IncCustomer.province

                };
                addAdress(newAdress);

                Debug.WriteLine("Test7");
                Debug.WriteLine("Test8");
            }
            return true;
        }
        public static CustomerModel find(int id)
        {
            var customerModel = new CustomerModel();
            using (var db = new CustomerContext())
            {
                try
                {
                    var temp = (from c in db.Customers
                                where c.customerID == id
                                select c).FirstOrDefault();

                    if (temp == null)
                        return null;
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
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                    //Environment.Exit(1);
                }
            }
            return null;
        }

        public static bool update(CustomerModel customerModel)
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var customer = (from c in db.Customers
                                    where c.customerID == customerModel.customerID
                                    select c).FirstOrDefault();
                    if (customer == null)
                        return false;

                    //Persondataendring:
                    customer.email = customerModel.email;
                    customer.firstName = customerModel.firstName;
                    customer.lastName = customerModel.lastName;
                    customer.phone = customerModel.phone;

                    customer.users.email = customerModel.email;
                    db.SaveChanges();

                    //Adresseendring:
                    var adressModel = new AdressModel();
                    adressModel.customerID = customerModel.customerID;
                    adressModel.deliveryAdress = true;
                    adressModel.payAdress = customerModel.sameAdresses;
                    adressModel.province = customerModel.province;
                    adressModel.streetName = customerModel.adress;
                    adressModel.zipCode = customerModel.zipCode;
                    addAdress(adressModel);
                    if (!customerModel.sameAdresses)
                    {
                        adressModel = new AdressModel();
                        adressModel.customerID = customerModel.customerID;
                        adressModel.deliveryAdress = false;
                        adressModel.payAdress = true;
                        adressModel.province = customerModel.payProvince;
                        adressModel.streetName = customerModel.payAdress;
                        adressModel.zipCode = customerModel.payZipcode;
                        addAdress(adressModel);
                    }

                    //***********************************************
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
            }
            return false;
        }

        public static string getProvince(string zipCode)
        {
            try
            {
                var db = new CustomerContext();
                var province = db.Provinces.Find(zipCode);
                return province.province;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
            }
            return null;
        }

        public static bool addAdress(AdressModel adressModel)/*Legger til ny adresse for bruker med customerID==adressModel.customerID. Alle felter unntatt adressID må være fylt ut*/
        {
            var adressesList = new List<Adresses>();
            if (adressModel.payAdress)
            {
                var temp = new Adresses();
                temp.payAdress = true;
                temp.deliveryAdress= false;
                temp.streetName = adressModel.streetName;
                temp.zipCode = adressModel.zipCode;
                adressesList.Add(temp);
            }
            if (adressModel.deliveryAdress)
            {
                var temp = new Adresses();
                temp.payAdress = false;
                temp.deliveryAdress = true;
                temp.streetName = adressModel.streetName;
                temp.zipCode = adressModel.zipCode;
                adressesList.Add(temp);
            }
            using (var db = new CustomerContext())
            {
                try
                {
                    //Kan fjernes hvis støtte for mer enn to adresser implementeres
                    var adresses = (from a in db.Adresses
                                    where a.customerID == adressModel.customerID
                                    select a).ToList();

                    foreach (var a in adresses)
                        foreach (var am in adressesList)
                            if (a.deliveryAdress == am.deliveryAdress && a.payAdress == am.payAdress)
                                db.Adresses.Remove(a);
                    //*****************************
                    foreach (var a in adressesList)
                    {
                        if (db.Provinces.Find(adressModel.zipCode) == null)
                        {
                            var province = new Provinces();
                            province.province = adressModel.province;
                            province.zipCode = adressModel.zipCode;
                            db.Provinces.Add(province);
                        }
                        a.province = db.Provinces.Find(adressModel.zipCode);
                        a.customers = db.Customers.Find(adressModel.customerID);
                        db.Adresses.Add(a);
                    }
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
                return false;
            }
        }

        public static bool addProvince(AdressModel adress)
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var temp = db.Provinces.Find(adress.zipCode);
                    if (temp == null)
                    {
                        temp = new Provinces();
                        temp.province = adress.province;
                        temp.zipCode = adress.zipCode;
                        db.Provinces.Add(temp);
                        return true;
                    }
                    db.SaveChanges();
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                    //Environment.Exit(1);
                }
            }

            return false;
        }

    }
}
