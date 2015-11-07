using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;

namespace Kaffeplaneten.Stubs
{
    public class EmployeeDALStub : IEmployeeDAL
    {
        public bool add(EmployeeModel employeeModel)
        {
            if (employeeModel.firstName != "")
                return true;
            return false;
        }
        public bool addAdress(AdressModel adressModel)
        {
            if (adressModel.personID > 0)
                return true;
            return false;
        }

        public bool addProvince(AdressModel adress)
        {
            if (adress.province != "")
                return true;
            return false;
        }

        public EmployeeModel find(int id)
        {
            if (id < 0)
                return null;
            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = 1;
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.customerAdmin = false;
            employeeModel.databaseAdmin = false;
            employeeModel.orderAdmin = true;
            employeeModel.employeeAdmin = false;
            employeeModel.password = "123456789";
            employeeModel.productAdmin = false;
            employeeModel.username = "Ola";
            return employeeModel;
        }

        public EmployeeModel find(string email)
        {
            if (email == "@kaffeplaneten.no")
                return null;
            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = 1;
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.customerAdmin = false;
            employeeModel.databaseAdmin = false;
            employeeModel.employeeAdmin = false;
            employeeModel.password = "123456789";
            employeeModel.productAdmin = false;
            employeeModel.username = "Ola";
            return employeeModel;
        }

        public List<EmployeeModel> getAllEmployees()
        {
            var employees = new List<EmployeeModel>();
            employees.Add(find(1));
            employees.Add(find(1));
            employees.Add(find(1));
            employees.Add(find(1));
            return employees;
        }

        public string getProvince(string zipCode)
        {
            if (zipCode == "")
                return "";
            return "Oslo";
        }

        public bool update(EmployeeModel employeeModel)
        {
            if (employeeModel.firstName != "")
                return true;
            return false;
        }
        public bool delete(int id)
        {
            if (id < 0)
                return false;
            return true;
        }
    }
}
