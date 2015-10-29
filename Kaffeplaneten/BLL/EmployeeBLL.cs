using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class EmployeeBLL
    {
        private EmployeeDAL _employeeDAL;
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

        public bool update(EmployeeModel employeeModel)//Oppdaterer employeen som har personID lik employeeModel.personID
        {
            return _employeeDAL.update(employeeModel);
        }

        public string getProvince(string zipCode)//Henter ut navnet på poststedet med postkode lik zipCode
        {
            return _employeeDAL.getProvince(zipCode);
        }

        public bool addAdress(AdressModel adressModel)//Legger til ny adresse for bruker med personID==adressModel.peronID. Alle felter unntatt adressID må være fylt ut
        {
            return _employeeDAL.addAdress(adressModel);            
        }

        public bool addProvince(AdressModel adress)//Legger en province inn i databasen dersom den ikke finnes fra før
        {
            return _employeeDAL.addProvince(adress);
        }
    }//end namespace
}//end class