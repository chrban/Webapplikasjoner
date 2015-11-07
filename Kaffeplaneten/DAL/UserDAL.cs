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
    public class UserDAL : IUserDAL
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
                    user.person = (from c in db.Customers
                                   where c.email.Equals(userModel.username)
                                   select c).SingleOrDefault();
                    if (user.person == null)//tester om Users sin customer finnes
                    {
                        user.person = (from e in db.Employees
                                       where e.email.Equals(userModel.username)
                                       select e).SingleOrDefault();
                        if (user.person == null)//tester om Users sin admin finnes
                        {
                            return false;
                        }
                    }
                    
                   
                    db.Users.Add(user);
                        db.SaveChanges();
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

        public bool resetPassword(UserModel userModel ,byte[] randomPW, bool customer )
        {
            using (var db = new CustomerContext())
            {
                Debug.WriteLine( randomPW + " motatt random pw: " + Encoding.Default.GetString(randomPW) );    

                userModel.passwordHash = randomPW;
                try
                {
                    var user = db.Users.Find(userModel.ID);

                    if (user == null)//tester om brukeren finnes
                        return false;

                    if (customer)
                    {
                        var customerID = db.Customers.Find(userModel.ID);

                        if (customerID == null)//tester om kunden finnes
                        {

                            return false;
                        }

                    }
                    else
                    {
                        var empID = db.Employees.Find(userModel.ID);
                        if (empID == null)//tester om kunden finnes
                        {
                            return false;
                        }
                    }


                    if (!userModel.username.Equals(user.username))
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
                    Debug.WriteLine("Gjennom!");

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                    Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                }
                return false;
            }//end using


            
        }//end resetPassword
    }//end namespace
}//end class
