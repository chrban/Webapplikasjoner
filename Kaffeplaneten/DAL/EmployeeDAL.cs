﻿using Kaffeplaneten.DAL;
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

        private LoggingDAL _logging;

        public EmployeeDAL()
        {
            _logging = new LoggingDAL();
        }

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
                    var newEmployee = new Employees()//Opretter ny employee
                    {
                        email = employeeModel.username + "@kaffeplaneten.no",
                        firstName = employeeModel.firstName,
                        lastName = employeeModel.lastName,
                        phone = employeeModel.phone,
                        employeeAdmin = employeeModel.employeeAdmin,
                        customerAdmin = employeeModel.customerAdmin,
                        productAdmin = employeeModel.productAdmin,
                        orderAdmin   = employeeModel.orderAdmin,
                        databaseAdmin = employeeModel.databaseAdmin
                    };
                    newEmployee = db.Employees.Add(newEmployee);
                    db.SaveChanges();
                    employeeModel.employeeID = newEmployee.personID;//Lagrer personID i modellen for senere bruk
                    return true;
                }
                catch (DbEntityValidationException dbEx)
                {
                    _logging.logToDatabase("FEIL: Databasevalidering når ansatt skulle bli lagt inn fikk feil!");
                }//end catch
            }//end using
            return false;
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
                    _logging.logToDatabase("FEIL: Klarte ikke finne ansatt med epost:" + email);
                }
                    return null;
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
                    employeeModel.orderAdmin = temp.orderAdmin;
                    employeeModel.employeeAdmin = temp.employeeAdmin;
                    employeeModel.productAdmin = temp.productAdmin;
                    employeeModel.databaseAdmin = temp.databaseAdmin;

                    return employeeModel;
                }//end try
                catch (Exception ex)
                {
                    /*Viser nyttig informasjon om alle excetions i debug.out. Avslutter programmet*/
                    _logging.logToDatabase("FEIL: Klarte ikke finne ansatt med ansattID:" + id);
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                    //Environment.Exit(1);
                }
            }//end using
            return null;
        }
        public List<EmployeeModel> getAllEmployees()
        {
            using (var db = new CustomerContext())
            {
                var EmployeeList = new List<EmployeeModel>();
                try
                {

                    var employees = (from e in db.Employees select e).ToList();
                    if (employees != null)
                        foreach (var e in employees)
                        {
                            var empModel = new EmployeeModel();
                            empModel.employeeID = e.personID;
                            empModel.firstName = e.firstName;
                            empModel.lastName = e.lastName;
                            empModel.phone = e.phone;
                            EmployeeList.Add(empModel);
                        }
                    return EmployeeList;
                }
                catch (Exception)
                {
                    _logging.logToDatabase("FEIL: Klarte ikke hente ut alle ansatte!");
                }
                return null;
            }
        }
        public bool delete(int id)
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    Users delUser = (from u in db.Users where u.personID == id select u).Single();
                    Employees delEmployee= (from e in db.Employees where e.personID == id select e).Single();
                    db.Users.Remove(delUser);
                    db.Employees.Remove(delEmployee);
                    //Adress, Orders og ProductOrder slettes automatisk
                    db.SaveChanges();
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
    }//end namespace
}//end class
