﻿using Kaffeplaneten.Controllers;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Kaffeplaneten
{
    public class TestingClass
    {
        public static void main(String[] args)
        {
            try
            {
                var customer = createCustomer();
                var order = createOrder();
                addCustomer(customer);
                addAdress(customer);
                addUser(customer);
                order.customerID = customer.customerID;
                addProduct(createProduct());
                order.orderNr = addOrder(order);
                //addProductOrder(order);
                //editCustomer(customer);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
            }
        }

        public static void addCustomer(CustomerModel customerModel)
        {
            var db = new CustomerContext();
            var customer = new Customers();
            customer.email = customerModel.email;
            customer.firstName = customerModel.firstName;
            customer.lastName = customerModel.lastName;
            customer.phone = customerModel.phone;
            db.Customers.Add(customer);
            db.SaveChanges();
            customerModel.customerID = customer.customerID;
        }
        public static void addAdress(CustomerModel customerModel)
        {
            var adress = new AdressModel();
            adress.deliveryAdress = true;
            adress.payAdress = true;
            adress.province = customerModel.province;
            adress.zipCode = customerModel.zipCode;
            adress.streetName = customerModel.adress;
            adress.customerID = customerModel.customerID;
            DBCustomer.addAdress(adress);

        }
        public static void addUser(CustomerModel customerModel)
        {
            var temp = new SuperController();
            var userModel = new UserModel();
            userModel.customerID = customerModel.customerID;
            userModel.passwordHash= temp.getHash(customerModel.password);
            userModel.username = customerModel.email;
            DBUser.add(userModel);
        }
        public static void addProduct(ProductModel productModel)
        {
            var db = new CustomerContext();
            var product = new Products();
            product.category = productModel.category;
            product.description = productModel.description;
            product.imageURL = productModel.imageURL;
            product.price = productModel.price;
            product.productName = productModel.productName;
            product.stock = productModel.stock;
            db.Products.Add(product);
            db.SaveChanges();
        }
        public static int addOrder(OrderModel orderModel)
        {
            DBOrder.add(orderModel);
            return orderModel.customerID;
        }
        public static void addProductOrder(OrderModel orderModel)
        {
            var db = new CustomerContext();
            var a = new int[100];
            foreach(var p in orderModel.products)
                a[p.productID]++;
            for(int i=0;i<a.Length;i++)
                if(a[i]>0)
                {
                    var productOrder = new ProductOrders();
                    productOrder.orders = db.Orders.Find(orderModel.orderNr);
                    productOrder.quantity = a[i];

                    productOrder.products = db.Products.Find(1);
                    productOrder.price = db.Products.Find(1).price * a[i];
                    db.ProductOrders.Add(productOrder);
                }
            foreach (var p in orderModel.products)
            {

            }
            db.SaveChanges();
        }

        public static CustomerModel createCustomer()
        {
            var customer = new CustomerModel();
            customer.firstName = "Ola";
            customer.lastName = "Nordmann";
            customer.password = "Gt123456789";
            customer.passwordVerifier = customer.password;
            customer.payAdress = "Bætaveien 2";
            customer.payZipcode = "1234";
            customer.payProvince = "Bætaland";
            customer.adress = customer.payAdress;
            customer.province = customer.payProvince;
            customer.zipCode = customer.payZipcode;
            customer.email = "hei@hei.hei";
            customer.phone = "12345678";
            return customer;
        }
        public static ProductModel createProduct()
        {
            var product = new ProductModel();
            product.category = "Kaffe";
            product.description = "God kaffe";
            product.imageURL = "kaffe.jpg";
            product.price = 10000;
            product.productName = "KaffeKaffe";
            product.stock = 1;
            product.productID = 1;
            return product;
        }
        public static OrderModel createOrder()
        {
            var order = new OrderModel();
            order.products.Add(createProduct());
            foreach (var p in order.products)
                p.quantity = 2;
            order.total=20000;
            return order;
        }
        public static bool editCustomer(CustomerModel customerModel)
        {
            customerModel.firstName="Trond";
            customerModel.email = "Trond@tronno.rønning";
            customerModel.adress = "Tronnoland";
            return DBCustomer.update(customerModel);
        }

    }
}