using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;


namespace Kaffeplaneten.DAL
{


    public class CustomerContext : DbContext
    {
        public CustomerContext()
            : base("name=DatabaseKaffeplaneten")
        {
            Database.CreateIfNotExists();
        }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Provinces> Provinces { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<ProductOrders> ProductOrders { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Adresses> Adresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
         {
            modelBuilder.Entity<ProductOrders>().HasKey(p => new { p.orderNr, p.productID });
            modelBuilder.Entity<Users>().HasKey(p => p.personID);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<Kaffeplaneten.DAL.Persons> Persons { get; set; }
    }
    public abstract class Persons
    {
        [Key]
        public int personID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }   
        public string phone { get; set; }
        public virtual Users users { get; set; }
    }

    public class Customers : Persons
    {
        public virtual List<Adresses> adresses { get; set; }
    }

    public class Employees : Persons
    {
        public bool employeeAdmin { get; set; }//Kan behandle ansatte og se admin loggen
        public bool databaseAdmin { get; set; }//Kan se på databaseloggen
        public bool productAdmin { get; set; }//Kan behandle produkter
        public bool orderAdmin { get; set; }//Kan behandle ordre
        public bool customerAdmin { get; set; }//Kan behandle kunder
    }

    public class Users
    {
        public int personID { get; set; }
        public string username { get; set; }
        public byte[] password { get; set; }
        [Required]
        public virtual Persons person { get; set; }
    }

    public class Adresses
    {
        [Key]
        public int adressID { get; set; }
        public bool payAdress { get; set; }
        public bool deliveryAdress { get; set; }
        public int personID { get; set; }
        public string zipCode { get; set; }
        public string streetName { get; set; }
        public virtual Persons person { get; set; }
        public virtual Provinces province { get; set; }
    }       


    public class Provinces
    {
        [Key]
        public string zipCode { get; set; }
        public string province { get; set; }
        public virtual List<Adresses> Adresses { get; set; }
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

    public class Orders
    {
        [Key]
        public int orderNr { get; set; }
        public int personID { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual List<ProductOrders> Products { get; set; }
    }

    public class ProductOrders
    {
        public int orderNr { get; set; }
        public int productID { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public virtual Orders orders { get; set; }
        public virtual Products products { get; set; }
    }
}

