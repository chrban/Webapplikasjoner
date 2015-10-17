using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaffeplaneten
{
    public class DBUser
    {

        public static bool add(UserModel userModel)//Legger en Users inn i databasen
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var user = (from u in db.Users
                                where u.email.Equals(userModel.username)
                                select u).FirstOrDefault();
                    if (user != null)
                        return false;
                    user = new Users()
                    {
                        email = userModel.username,
                        password = userModel.passwordHash
                    };
                    user.customer = db.Customers.Find(userModel.customerID);
                    if (user.customer == null)//tester om Users sin customer finnes
                        return false;
                    db.Users.Add(user);
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

        public static UserModel get(string email)//henter ut en UserModel med Users.email lik email
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var user = (from u in db.Users
                                where u.email.Equals(email)
                                select u).FirstOrDefault();
                    if (user == null)//tester om brukeren finnes
                        return null;

                    var userModel = new UserModel();
                    userModel.customerID = user.customerID;
                    userModel.passwordHash = user.password;
                    userModel.username = user.email;
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
        public static bool update(UserModel userModel)//Oppdaterer Users data med dataen i userModel
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var user = db.Users.Find(userModel.customerID);
                    if (user == null)//tester om brukeren finnes
                        return false;
                    var customer = db.Customers.Find(userModel.customerID);
                    if (customer == null)//tester om kunden finnes
                        return false;
                    if(!userModel.username.Equals(user.email))
                    {
                        var email = (from p in db.Users
                                     where p.email.Equals(userModel.username)
                                     select p).FirstOrDefault();
                        if (email != null)//tester om epostadressen finnes fra før
                            return false;
                        user.email = userModel.username;
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

        public static bool verifyUser(UserModel userModel)
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var user = (from u in db.Users
                                where u.password == userModel.passwordHash && u.email == userModel.username
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
        public static UserModel get(int id)//henter ut en UserModel fra User med customerID lik id
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var userModel = new UserModel();
                    var user = db.Users.Find(id);
                    if (user == null)
                        return null;
                    userModel.customerID = id;
                    userModel.passwordHash = user.password;
                    userModel.username = user.email;
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
