namespace Kaffeplaneten
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;


    public class CustomerContext : DbContext
    {

        public CustomerContext()
            : base("name=DatabaseKaffeplaneten")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Persons> Customers { get; set; }
        public DbSet<Provinces> Provinces { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Provinces>().HasKey(p => p.zipCode);
            modelBuilder.Entity<ProductOrders>().HasKey(p => new { p.orderNr, p.productID });
            modelBuilder.Entity<Adresses>().HasKey(p => new { p.customerID, p.zipCode });
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
    public class Persons
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        [Key]
        public string email { get; set; }
        public string phone { get; set; }


    }

    public class Customers:Persons
    {
        [Key]
        public int customerID { get; set; }
        public virtual List<Adresses> adresses { get; set; }
        public virtual List<Orders> orders { get; set; }
    }

    public class Adresses
    {
        public bool payAdress { get; set; }
        public bool deliveryAdress { get; set; }
        public int customerID { get; set; }
        public string zipCode { get; set; }
        public string streetName { get; set; }
        public virtual Customer customer { get; set; }
        public virtual Provinces province { get; set; }
    }

    public class Provinces
    {
        [Key]
        public string zipCode { get; set; }
        public string province { get; set; }
        public virtual List<Adresses> adresses { get; set; }

    }

    public class Products
    {
        [Key]
        public int productID { get; set; }
        public string imageURL { get; set; }
        public string productName { get; set; }
        public int stock { get; set; }
        public double price { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public virtual List<ProductOrders> productOrders { get; set; }

    }

    public class ProductOrders
    {
        //[Key]
        public int orderNr { get; set; }
        //[Key]
        public int productID { get; set; }
        public int quantity { get; set; }
    }

    public class Orders
    {
        [Key]
        public int orderNr { get; set; }
        public int customerID { get; set; }
        public virtual Customer Customers { get; set; }
        public virtual List<ProductOrders> productOrders { get; set; }

    }

}

