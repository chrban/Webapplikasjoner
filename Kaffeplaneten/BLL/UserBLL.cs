using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaffeplaneten.BLL
{
    public class UserBLL
    {

        private IUserDAL _userDAL;
        private LoggingBLL _loggingBLL;
        public UserBLL(IUserDAL iUserDAL)
        {
            _userDAL = iUserDAL;
            _loggingBLL = new LoggingBLL();
        }
        public UserBLL()
        {
            _userDAL = new UserDAL();
            _loggingBLL = new LoggingBLL();
        }
        public bool add(UserModel userModel)//Legger en Users inn i databasen
        {
            _loggingBLL.logToUser("Opprettet: " + userModel.username);
            _loggingBLL.logToDatabase("Ny bruker ble lagt til: " + userModel.username);
            return _userDAL.add(userModel);
        }

        public UserModel get(string email)//henter ut en UserModel med Users.email lik email
        {
            return _userDAL.get(email);
        }
        public bool update(UserModel userModel)//Oppdaterer Users data med dataen i userModel
        {
            _loggingBLL.logToUser("Oppdaterte bruker: " + userModel.username + " (BrukerID: " + userModel.ID + ")");
            _loggingBLL.logToDatabase("Bruker: " + userModel.username + " (BrukerID: " + userModel.ID + ") ble oppdatert.");
            return _userDAL.update(userModel);
        }

        public bool verifyUser(UserModel userModel)//Bekrefter brukernavn og passord for user
        {
            return _userDAL.verifyUser(userModel);
        }
        public UserModel get(int id)//henter ut en UserModel fra User med customerID lik id
        {
            return _userDAL.get(id);
        }//end get()
    }//end namespace
}//end class
