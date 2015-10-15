﻿using Kaffeplaneten.Models;
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
        public static void main(String[]args)
        {
            try
            {
                var db = new CustomerContext();
                var customer = new Customers();
                var customerModel = createCustomer();
                customer.email = customerModel.email;
                customer.firstName = customerModel.firstName;
                customer.lastName = customerModel.lastName;
                customer.phone = customerModel.phone;
                db.Customers.Add(customer);
                /*********/
                var province = new Provinces();
                province.province = customerModel.payProvince;
                province.zipCode = customerModel.payZipcode;
                db.Provinces.Add(province);
                /*********/
                var adress = new Adresses();
                adress.deliveryAdress = true;
                adress.payAdress = true;
                adress.province = province;
                adress.streetName = customerModel.adress;
                adress.customers = customer;
                db.Adresses.Add(adress);
                /*********/
                var user = new Users();
                user.email = customerModel.email;
                user.password = new byte[10];
                user.customer = customer;
                db.Users.Add(user);
                /********/
                var productModel = createProduct();
                var product = new Products();
                product.category = productModel.category;
                product.description = productModel.description;
                product.imageURL = productModel.imageURL;
                product.price = productModel.price;
                product.productName = productModel.productName;
                product.stock = productModel.stock;
                db.Products.Add(product);
                /********/
                Orders order = new Orders();
                order.Customers = customer;
                db.Orders.Add(order);
                /*******/
                var productOrder = new ProductOrders();
                productOrder.orders = order;
                productOrder.products = product;
                productOrder.quantity = 2;
                productOrder.price = product.price * 2;
                db.ProductOrders.Add(productOrder);
                db.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

        }
        public static CustomerModel createCustomer()
        {
            var customer = new CustomerModel();
            customer.firstName = "Ola";
            customer.lastName = "Nordmann";
            customer.password = "123";
            customer.passwordVerifier = customer.password;
            customer.payAdress = "Bætaveien 2";
            customer.payZipcode = "1234";
            customer.payProvince = "Bætaland";
            customer.adress = customer.payAdress;
            customer.province = customer.payProvince;
            customer.zipCode = customer.payZipcode;
            customer.email = "hei@hei.hei";
            customer.username = customer.email;
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
            return product;
        }
        public static OrderModel createOrder()
        {
            var order = new OrderModel();
            order.products.Add(createProduct());
            order.products.Add(createProduct());
            order.total=20000;
            return order;
        }
    }
}