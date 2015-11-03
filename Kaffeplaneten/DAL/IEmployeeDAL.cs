using Kaffeplaneten.Models;

namespace Kaffeplaneten.DAL
{
    public interface IEmployeeDAL
    {
        bool add(EmployeeModel employeeModel);
        EmployeeModel find(string email);
        EmployeeModel find(int id);
        bool update(EmployeeModel employeeModel);
    }
}