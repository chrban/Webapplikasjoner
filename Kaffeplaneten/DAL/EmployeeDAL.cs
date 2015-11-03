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
    public class EmployeeDAL : IEmployeeDAL
    {
        public bool add(EmployeeModel employeeModel)//Legger employee inn i datatbasen
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    if (!(db.Employees.Find(employeeModel.employeeID) == null))//Hvis employeeModel har personID som finnes fra før
                        return false;
                    if (find(employeeModel.username) != null)
                        return false;
                    var newEmployee = new Employee()//Opretter ny employee
                    {
                        email = employeeModel.username,
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
                    employeeModel.username = temp.email;
                    employeeModel.phone = temp.phone;
                    employeeModel.customerAdmin = temp.customerAdmin;
                    employeeModel.databaseAdmin = temp.databaseAdmin;
                    employeeModel.employeeAdmin = temp.employeeAdmin;
                    employeeModel.productAdmin = temp.productAdmin;
         
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
                    employee.email = employeeModel.username;
                    employee.firstName = employeeModel.firstName;
                    employee.lastName = employeeModel.lastName;
                    employee.phone = employeeModel.phone;
                    employee.customerAdmin = employeeModel.customerAdmin;
                    employee.databaseAdmin = employeeModel.databaseAdmin;
                    employee.employeeAdmin = employeeModel.employeeAdmin;
                    employee.productAdmin = employeeModel.productAdmin;

                    employee.users.username = employeeModel.username;
                    db.SaveChanges();

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
    }//end namespace
}//end class
