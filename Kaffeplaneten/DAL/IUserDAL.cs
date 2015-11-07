using Kaffeplaneten.Models;

namespace Kaffeplaneten.DAL
{
    public interface IUserDAL
    {
        bool add(UserModel userModel);
        UserModel get(string email);
        UserModel get(int id);
        bool update(UserModel userModel);
        bool verifyUser(UserModel userModel);
        bool resetPassword(UserModel user, string randomPW);
    }
}