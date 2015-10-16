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
    public class DBuser
    {

        public bool add(byte[] IncPassword, Customers newCustomer, CustomerContext db)
        {
            try
            {
                Debug.WriteLine("Test1,5");
                Debug.WriteLine("Test2");
                var newUser = new Users()
                {
                    email = newCustomer.email,
                    password = IncPassword
                };
                newUser.customer = newCustomer;
                db.Users.Add(newUser);
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
                    }
                }
                return false;

            }
        }
        public static bool update(UserModel userModel)
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var user = db.Users.Find(userModel.customerID);
                    if (user == null)
                        return false;
                    var customer = db.Customers.Find(userModel.customerID);
                    if (customer == null)
                        return false;
                    if(!userModel.username.Equals(user.email))
                    {
                        var email = (from p in db.Users
                                     where p.email.Equals(userModel.username)
                                     select p).FirstOrDefault();
                        if (!(email == null))
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
        public static UserModel get(int id)
        {
            using (var db = new CustomerContext())
            {
                try
                {
                    var userModel = new UserModel();
                    var user = db.Users.Find(id);
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
            }
        }
    }
}
