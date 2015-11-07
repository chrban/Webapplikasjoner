using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaffeplaneten.BLL
{
    public class EmployeeBLL
    {
        private IEmployeeDAL _employeeDAL;
        public EmployeeBLL(IEmployeeDAL iEmployeeDAL)
        {
            _employeeDAL = iEmployeeDAL;
        }
        public EmployeeBLL()
        {
            _employeeDAL = new EmployeeDAL();
        }
        public bool add(EmployeeModel employeeModel)//Legger employee inn i datatbasen
        {
            return _employeeDAL.add(employeeModel);
        }

        public EmployeeModel find(string email)//Henter ut navn på bruker med brukernavn lik email
        {
            return _employeeDAL.find(email);
        }
        public EmployeeModel find(int id)//Henter ut en EmployeeModel for employee med personID lik id
        {
            return _employeeDAL.find(id);
        }
        public Boolean delete(int id)
        {
            return _employeeDAL.delete(id);
        }
        public bool update(EmployeeModel employeeModel)//Oppdaterer employeen som har personID lik employeeModel.personID
        {
            return _employeeDAL.update(employeeModel);
        }
        public List<EmployeeModel> getAllEmployees()
        {
            return _employeeDAL.getAllEmployees();
        }
    }//end namespace
}//end class