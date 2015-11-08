using Kaffeplaneten.Models;
using System.Collections.Generic;

namespace Kaffeplaneten.DAL
{
    public interface IEmployeeDAL
    {
        bool add(EmployeeModel employeeModel);
        EmployeeModel find(string email);
        EmployeeModel find(int id);
        bool delete(int id);
        List<EmployeeModel> getAllEmployees();
    }
}