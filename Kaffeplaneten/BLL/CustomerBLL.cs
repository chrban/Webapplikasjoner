﻿using Kaffeplaneten.Models;
using Kaffeplaneten.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace Kaffeplaneten.BLL
{
    public class CustomerBLL
    {
        private ICustomerDAL _customerDAL;
        private LoggingBLL _loggingBLL;
        public CustomerBLL(ICustomerDAL iCustomerDAL)
        {
            _customerDAL = iCustomerDAL;
            _loggingBLL = new LoggingBLL();
        }
        public CustomerBLL()
        {
            _customerDAL = new CustomerDAL();
            _loggingBLL = new LoggingBLL();
        }
        public bool add(CustomerModel IncCustomer)//Legger customer inn i datatbasen
        {
            return _customerDAL.add(IncCustomer);
        }

        public CustomerModel find(string email)//Henter ut navn på bruker med brukernavn lik email
        {
            return _customerDAL.find(email);
        }
        public CustomerModel find(int id)//Henter ut en CustomerModel for customer med customerID lik id
        {
            return _customerDAL.find(id);
        }

        public bool update(CustomerModel customerModel)//Oppdaterer customeren som har customerID lik customerModel.customerID
        {
            return _customerDAL.update(customerModel);
        }

        public string getProvince(string zipCode)//Henter ut navnet på poststedet med postkode lik zipCode
        {
            return _customerDAL.getProvince(zipCode);
        }

        public bool addAdress(AdressModel adressModel)//Legger til ny adresse for bruker med customerID==adressModel.customerID. Alle felter unntatt adressID må være fylt ut
        {
            return _customerDAL.addAdress(adressModel);
        }

        public bool addProvince(AdressModel adress)//Legger en province inn i databasen dersom den ikke finnes fra før
        {
            return _customerDAL.addProvince(adress);
        }

        public List<CustomerModel> allCustomers()
        {
           return  _customerDAL.allCustomers();
        }
        public bool delete(int id)
        {
            return _customerDAL.delete(id);
        }



    }//end namespace
}//end class
