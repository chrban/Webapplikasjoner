using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;

namespace Stubs
{
    class EmployeeDALStup : IEmployeeDAL
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
            var employeeModel = new EmployeeModel();
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            return employeeModel;
        }

        public EmployeeModel find(string email)
        {
            var employeeModel = new EmployeeModel();
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            return employeeModel;
        }

        public string getProvince(string zipCode)
        {
            return "Oslo";
        }

        public bool update(EmployeeModel employeeModel)
        {
            if (employeeModel.firstName != "")
                return true;
            return false;
        }
    }
}
