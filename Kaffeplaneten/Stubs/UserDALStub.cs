using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;

namespace Kaffeplaneten.Stubs
{
    public class UserDALStub : IUserDAL
    {
        public bool add(UserModel userModel)
        {
            if (userModel.ID > 0)
                return true;
            return false;
        }

        public UserModel get(int id)
        {
            if (id < 0)
                return null;
            var userModel = new UserModel();
            userModel.ID = 1;
            userModel.password = "123456789";
            userModel.username = "Ola@nordmann.no";
            return userModel;
        }

        public UserModel get(string email)
        {
            if (email == "@kaffeplaneten.no")
                return null;
            return get(1);
        }

        public bool resetPassword(UserModel user, byte[] randomPW)
        {
            if (user.ID < 0)
                return false;
            return true;
        }

        public bool resetPassword(UserModel user, byte[] randomPW, bool customer)
        {
            if (user.ID < 0)
                return false;
            return true;
        }

        public bool update(UserModel userModel)
        {
            if (userModel.ID > 0)
                return true;
            return false;
        }

        public bool verifyUser(UserModel userModel)
        {
            if (userModel.ID > 0)
                return true;
            return false;
        }
    }
}
