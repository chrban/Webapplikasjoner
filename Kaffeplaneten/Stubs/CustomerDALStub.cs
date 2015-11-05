using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;

namespace Stubs
{
    class CustomerDALStub : ICustomerDAL
    {
        public bool add(CustomerModel IncCustomer)
        {
            if (IncCustomer.firstName != "")
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

        public List<CustomerModel> allCustomers()
        {
            var customers = new List<CustomerModel>();
            customers.Add(find(1));
            customers.Add(find(1));
            customers.Add(find(1));
            customers.Add(find(1));
            return customers;
        }

        public bool delete(int id)
        {
            if (id > 0)
                return true;
            return false;
        }

        public CustomerModel find(int id)
        {
            var customerModel = new CustomerModel();
            customerModel.firstName = "Ola";
            customerModel.lastName = "Nordmann";
            customerModel.payAdress = "Osloveien 1";
            customerModel.payProvince = "Oslo";
            customerModel.payZipcode = "1234";
            customerModel.phone = "12345678";
            customerModel.province = "Oslo";
            customerModel.sameAdresses = true;
            customerModel.zipCode = "1234";
            customerModel.adress = "Osloveien 1";
            return customerModel;
        }

        public CustomerModel find(string email)
        {
            var customerModel = new CustomerModel();
            customerModel.firstName = "Ola";
            customerModel.lastName = "Nordmann";
            customerModel.payAdress = "Osloveien 1";
            customerModel.payProvince = "Oslo";
            customerModel.payZipcode = "1234";
            customerModel.phone = "12345678";
            customerModel.province = "Oslo";
            customerModel.sameAdresses = true;
            customerModel.zipCode = "1234";
            customerModel.adress = "Osloveien 1";
            return customerModel;
        }

        public string getProvince(string zipCode)
        {
            return "Oslo";
        }

        public bool update(CustomerModel customerModel)
        {
            if (customerModel.firstName != "")
                return true;
            return false;
        }
    }
}
