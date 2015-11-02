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
    public class UserDAL
    {
        public bool add(UserModel userModel)//Legger en Users inn i databasen
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var user = (from u in db.Users
                                where u.username.Equals(userModel.username)
                                select u).FirstOrDefault();
                    if (user != null)
                        return false;

                    user = new Users()
                    {
                        username = userModel.username,
                        password = userModel.passwordHash
                    };
                    user.person = db.Employees.Find(userModel.ID);
                    if (user.person == null)//tester om Users sin customer/Admin finnes
                    {
                        Debug.WriteLine("Test2");
                        user.person = db.Customers.Find(userModel.ID);
                        if (user.person == null)
                            return false;
                    }
                    Debug.WriteLine("Test33!!");
                    db.Users.Add(user);
                    Debug.WriteLine("Test4!!!");
                    db.SaveChanges();
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
                        }//end foreach
                    }//end foreach
                }//end catch
                return false;
            }//end using
        }

        public UserModel get(string email)//henter ut en UserModel med Users.email lik email
        {
            var userModel = new UserModel();
            using (var db = new CustomerContext())
            {
                try
                {
                    var user = (from u in db.Users
                                where u.username.Equals(email)
                                select u).FirstOrDefault();
                    if (user == null)//tester om brukeren finnes
                        return null;

                    userModel.ID = user.personID;
                    userModel.passwordHash = user.password;
                    userModel.username = user.username;
                    return userModel;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
                return null;
            }
        }
        public bool update(UserModel userModel)//Oppdaterer Users data med dataen i userModel
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var user = db.Users.Find(userModel.ID);
                    if (user == null)//tester om brukeren finnes
                        return false;
                    var customer = db.Customers.Find(userModel.ID);
                    if (customer == null)//tester om kunden finnes
                        return false;
                    if(!userModel.username.Equals(user.username))
                    {
                        var email = (from p in db.Users
                                     where p.username.Equals(userModel.username)
                                     select p).FirstOrDefault();
                        if (email != null)//tester om epostadressen finnes fra før
                            return false;
                        user.username = userModel.username;
                    }
                    user.password = userModel.passwordHash;
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

        public bool verifyUser(UserModel userModel)//Bekrefter brukernavn og passord for user
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var user = (from u in db.Users
                                where u.password == userModel.passwordHash && u.username == userModel.username
                                select u).SingleOrDefault();
                    if (user == null)
                        return false;
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
                return false;
            }//end using
        }
        public UserModel get(int id)//henter ut en UserModel fra User med customerID lik id
        {
            var userModel = new UserModel();
            using (var db = new CustomerContext())
            {
                try
                {
                    var user = db.Users.Find(id);
                    if (user == null)
                        return null;
                    userModel.ID = id;
                    userModel.passwordHash = user.password;
                    userModel.username = user.username;
                    return userModel;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
                return null;
            }//end using
        }//end get()
    }//end namespace
}//end class
