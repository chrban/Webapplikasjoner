using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;

namespace Stubs
{
    class UserDALStub : IUserDAL
    {
        public bool add(UserModel userModel)
        {
            if (userModel.ID > 0)
                return true;
            return false;
        }

        public UserModel get(int id)
        {
            var userModel = new UserModel();
            userModel.ID = 1;
            userModel.password = "123456789";
            userModel.username = "Ola@nordmann.no";
            return userModel;
        }

        public UserModel get(string email)
        {
            return get(1);
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
