using Kaffeplaneten.Models;
using System.Collections.Generic;

namespace Kaffeplaneten.DAL
{
    public interface ICustomerDAL
    {
        bool add(CustomerModel IncCustomer);
        bool addAdress(AdressModel adressModel);
        bool addProvince(AdressModel adress);
        CustomerModel find(string email);
        CustomerModel find(int id);
        string getProvince(string zipCode);
        bool update(CustomerModel customerModel);
        List<CustomerModel> allCustomers();
        bool delete(int id);
    }
}