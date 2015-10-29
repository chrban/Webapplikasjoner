using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaffeplaneten.DAL
{
    public class EmployeeDAL
    {
        public bool add(EmployeeModel employeeModel)//Legger employee inn i datatbasen
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    if (!(db.Employees.Find(employeeModel.employeeID) == null))//Hvis employeeModel har personID som finnes fra før
                        return false;
                    var newEmployee = new Employee()//Opretter ny employee
                    {
                        email = employeeModel.email,
                        firstName = employeeModel.firstName,
                        lastName = employeeModel.lastName,
                        phone = employeeModel.phone,
                        employeeAdmin = employeeModel.employeeAdmin,
                        customerAdmin = employeeModel.customerAdmin,
                        productAdmin = employeeModel.productAdmin,
                        databaseAdmin=employeeModel.databaseAdmin
                    };
                    newEmployee = db.Employees.Add(newEmployee);
                    db.SaveChanges();
                    employeeModel.employeeID = newEmployee.personID;//Lagrer personID i modellen for senere bruk
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

            var adressModel = new AdressModel()
            {
                personID = employeeModel.employeeID,
                payAdress = false,
                deliveryAdress = false,
                zipCode = employeeModel.zipCode,
                streetName = employeeModel.adress,
                province = employeeModel.province
            };
            addAdress(adressModel);

            return true;
        }

        public EmployeeModel find(string email)//Henter ut navn på bruker med brukernavn lik email
        {
            var employeeModel = new EmployeeModel();
            using (var db = new CustomerContext())
            {
                try
                {
                    var temp = (from c in db.Employees
                                where c.email == email
                                select c).SingleOrDefault();
                    if (temp == null)
                        return null;
                    return find(temp.personID);

                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public EmployeeModel find(int id)//Henter ut en EmployeeModel for employee med personID lik id
        {
            var employeeModel = new EmployeeModel();
            using (var db = new CustomerContext())
            {
                try
                {
                    var temp = (from c in db.Employees
                                where c.personID == id
                                select c).FirstOrDefault();

                    if (temp == null)//Tester om employeen finnes
                        return null;
                    employeeModel.employeeID = temp.personID;
                    employeeModel.firstName = temp.firstName;
                    employeeModel.lastName = temp.lastName;
                    employeeModel.email = temp.email;
                    employeeModel.phone = temp.phone;
                    employeeModel.customerAdmin = temp.customerAdmin;
                    employeeModel.databaseAdmin = temp.databaseAdmin;
                    employeeModel.employeeAdmin = temp.employeeAdmin;
                    employeeModel.productAdmin = temp.productAdmin;

                    Adresses adress = (from a in db.Adresses
                                               where a.personID == employeeModel.employeeID
                                               select a).FirstOrDefault();

                    employeeModel.adress = adress.streetName;
                    employeeModel.province = adress.province.province;
                    employeeModel.zipCode = adress.zipCode;
         
                    return employeeModel;
                }//end try
                catch (Exception ex)
                {
                    /*Viser nyttig informasjon om alle excetions i debug.out. Avslutter programmet*/
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                    //Environment.Exit(1);
                }
            }//end using
            return null;
        }

        public bool update(EmployeeModel employeeModel)//Oppdaterer employeen som har personID lik employeeModel.personID
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var employee = (from c in db.Employees
                                    where c.personID == employeeModel.employeeID
                                    select c).FirstOrDefault();
                    if (employee == null)//tester om employeen finnes
                        return false;

                    //Persondataendring:
                    employee.email = employeeModel.email;
                    employee.firstName = employeeModel.firstName;
                    employee.lastName = employeeModel.lastName;
                    employee.phone = employeeModel.phone;
                    employee.customerAdmin = employeeModel.customerAdmin;
                    employee.databaseAdmin = employeeModel.databaseAdmin;
                    employee.employeeAdmin = employeeModel.employeeAdmin;
                    employee.productAdmin = employeeModel.productAdmin;

                    employee.users.email = employeeModel.email;
                    db.SaveChanges();

                    //Adresseendring:
                    var adressModel = new AdressModel();
                    adressModel.personID = employeeModel.employeeID;
                    adressModel.deliveryAdress = true;
                    adressModel.province = employeeModel.province;
                    adressModel.streetName = employeeModel.adress;
                    adressModel.zipCode = employeeModel.zipCode;
                    addAdress(adressModel);

                    return true;
                }//emd try
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
            }//end using
            return false;
        }

        public string getProvince(string zipCode)//Henter ut navnet på poststedet med postkode lik zipCode
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var province = db.Provinces.Find(zipCode);
                    if (province == null)
                        return null;
                    return province.province;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
            }//end using
            return null;
        }

        public bool addAdress(AdressModel adressModel)//Legger til ny adresse for bruker med personID==adressModel.peronID. Alle felter unntatt adressID må være fylt ut
        {

            var adress = new Adresses();
            adress.payAdress = true;
            adress.deliveryAdress = false;
            adress.streetName = adressModel.streetName;
            adress.zipCode = adressModel.zipCode;

            using (var db = new CustomerContext())
            {
                try
                {
                    //Fjerner tidligere adresse
                    var adresses = (from a in db.Adresses
                                    where a.personID == adressModel.personID
                                    select a).ToList();

                    foreach (var a in adresses)
                        db.Adresses.Remove(a);


                    addProvince(adressModel);
                    adress.province = db.Provinces.Find(adressModel.zipCode);
                    adress.person = db.Employees.Find(adressModel.personID);
                    db.Adresses.Add(adress);

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

        public bool addProvince(AdressModel adress)//Legger en province inn i databasen dersom den ikke finnes fra før
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
                        db.SaveChanges();
                        return true;
                    }
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
    }//end namespace
}//end class
