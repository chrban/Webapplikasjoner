namespace Kaffeplaneten.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class CustomerContext : DbContext
    {


        public CustomerContext()
            : base("name=CustomerContext")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Persons> Persons{ get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Provinces> Provinces{ get; set; }
        public DbSet<Products> Products{ get; set; }
        public DbSet<Orders> Orders{ get; set; }

    }

    public class Persons
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }


    }

    public class Customers : Persons
    {
        public int customerID { get; set; }
        public string zipCode { get; set; }
        public string adress { get; set; }
        public string payAdress { get; set; }
        public string payZipCode { get; set; }
        public string payProvince { get; set; }
        public virtual Provinces provinces{ get; set; }
    }

    public class Provinces
    {
        public string zipCode { get; set; }
        public string province { get; set; }
        public virtual List<Customer> Customers { get; set; }
        
    }

    public class Products
    {
        public int productID{ get; set; }
        public string imageURL { get; set; }
        public string productName { get; set; }
        public int stock { get; set; }
        public double price { get; set; }
        public string category { get; set; }
        public string description { get; set; }

    }

    public class Orders
    {
        public int orderNr{ get; set; }
        public virtual Customer Customers { get; set; }
        public virtual List<Products> Products { get; set; }

    }




}