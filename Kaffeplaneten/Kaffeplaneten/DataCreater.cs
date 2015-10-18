using Kaffeplaneten.Controllers;
using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Kaffeplaneten
{
    public class DataCreater
    {
        public static void main(String[] args)
        {
            addProducts();
        }
        public bool createTestDatabase()
        {
            try
            {
                var customer = createCustomer();
                var order = createOrder();
                addCustomer(customer);
                addAdress(customer);
                addUser(customer);
                order.customerID = customer.customerID;
                addProducts();
                order.orderNr = addOrder(order);
                addProductOrder(order);
                //editCustomer(customer);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
                return false;
            }
        }

        public static void addProducts()
        {
            try
            {
                addProduct(createAfterDinnerBlend());
                addProduct(createAstorLibano());
                addProduct(createBrazillianBlend());
                addProduct(createCafedeParis());
                addProduct(createChocoVanilje());
                addProduct(createCostaRicanTarrazu());
                addProduct(createEtiopiskMokka());
                addProduct(createExecutiveBlend());
                addProduct(createIndianMysore());
                addProduct(createIrishCream());
                addProduct(createKoffeinfriEspresso());
                addProduct(createKoffeinfriKaffe());
                addProduct(createMayfairBlend());
                addProduct(createMexicanCoffee());
                addProduct(createNutCream());
                addProduct(createOldBrownJava());
                addProduct(createPrimeHonduras());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\nERROR!\nMelding:\n" + ex.Message + "\nInner exception:" + ex.InnerException + "\nKastet fra\n" + ex.TargetSite + "\nTrace:\n" + ex.StackTrace);
                Trace.TraceInformation("Property: {0} Error: {1}", ex.Source, ex.InnerException);
            }
        }

        public static string getRandomImage()
        {
            string img = "img";
            string jpg = ".jpg";
            Random rnd = new Random();
            return (img + rnd.Next(1, 6) + jpg);
        }

        public static ProductModel createBrazillianBlend()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Brazillian Blend";
            p.description = "Vår rimelige hverdagskaffe. Lett fin frokostkaffe nesten helt uten garvesyre. Har du dårlig \"kaffemage\" er dette definitivt din kaffe.";
            p.price = 45;
            p.category = "Lettbrent";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createEtiopiskMokka()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Etiopisk Mokka";
            p.description = "Lyst brent kaffe med snev av vill ettersmak. Fin frokostkaffe. Meget populær.";
            p.price = 52;
            p.category = "Lettbrent";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createIndianMysore()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Indian Mysore";
            p.description = "Flott kaffe fra det sydlige India. God syrlighet og stor aroma. Lang ettersmak. En super kaffe.";
            p.price = 59;
            p.category = "Lettbrent";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createAstorLibano()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Astor Libano";
            p.description = "Dette er en kaffe du må unne deg. Meduim fyldig, frisk syrlighet og stor dybde og aroma. En fantastisk flott kaffe.";
            p.price = 55;
            p.category = "Mediumbrent";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createMayfairBlend()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Mayfair Blend";
            p.description = "Medium fyldig kaffe. Allround kaffen som passer til det meste. Meget god blanding.";
            p.price = 49;
            p.category = "Mediumbrent";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createMexicanCoffee()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Mexican Coffee";
            p.description = "Antydning til røyksmak. Fin sortering av ekstra store bønner. Medium til fyldig.";
            p.price = 59;
            p.category = "Mediumbrent";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createExecutiveBlend()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Executive Blend";
            p.description = "Moka d'Or og High mountain blend blandet. Medium mørk med en fin avrundet smak.";
            p.price = 49;
            p.category = "Mediumbrent";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createAfterDinnerBlend()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "After Dinner Blend";
            p.description = "Mørkbrent kaffe med 25% robusta. Dette gir en typisk \"europeisk\" smak kraftig og med antydning til bitter ettersmak. Ikke bare en ettermiddagskaffe, ypperlig også til å våkne på - gjerne med melk og brunt sukker.";
            p.price = 48;
            p.category = "Mørk";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createCafedeParis()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Cafe de Paris";
            p.description = "Meget mørk og kraftig kaffe. Bare for de aller tøffeste. Populær. Ypperlig også til Caffe Latte og Irish Coffee.";
            p.price = 48;
            p.category = "Mørk";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createCostaRicanTarrazu()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Costa Rican Tarrazu";
            p.description = "Mørk kaffe av aller fineste kvalitet. Passer i selskap til kaker og/eller cognac. Hva med en sigar også?";
            p.price = 59;
            p.category = "Mørk";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createPrimeHonduras()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Prime Honduras";
            p.description = "En singel plantasjekaffe av høy kvalitet. Mørk og fyldig. Vår bestselger!";
            p.price = 52;
            p.category = "Mørk";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createOldBrownJava()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Old Brown Java";
            p.description = "Dette er en kaffe mange har ventet på å få tilbake etter flere år i eksil. Den er kraftig med lang ettersmak og rik på undertoner av lær og tobakk. En utsøkt kaffe for de som vil ha det kraftfullt.";
            p.price = 59;
            p.category = "Mørk";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createKoffeinfriEspresso()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Koffeinfri Espresso";
            p.description = "Et godt alternativ til de som ikke kan ha koffein eller som rett og slett bare vil bevare sin gode nattesøvn.";
            p.price = 59;
            p.category = "Koffeinfri";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createKoffeinfriKaffe()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Koffeinfri Kaffe";
            p.description = "Hvis ikke du vet at dette er koffeinfri kaffe, ville du aldri tro det. Smaker helt som kaffe med koffein. Dette er dessuten kaffe av høyeste kvalitet som utgangspunkt. Vi våger påstanden: Du har aldri smakt bedre koffeinfri.";
            p.price = 59;
            p.category = "Koffeinfri";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createChocoVanilje()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Choco Vanilje";
            p.description = "Kaffe tilsatt sjokolade og vaniljesmak.";
            p.price = 69;
            p.category = "Aromafisert";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel VaniljeHasselnøtt()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Vanilje Hasselnøtt";
            p.description = "Populær blanding av nøtt og vaniljesmak.";
            p.price = 69;
            p.category = "Aromafisert";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createNutCream()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Nut Cream";
            p.description = "Kaffe tilsatt aroma med nøtt og fløtesmak. Meget god og populær kaffe.";
            p.price = 65;
            p.category = "Aromafisert";
            p.stock = 50;
            p.imageURL =getRandomImage();
            return p;
        }
        public static ProductModel createIrishCream()
        {
            var p = new ProductModel(); p.imageURL = getRandomImage();
            p.productName = "Irish Cream";
            p.description = "Kaffe tilsatt aroma med Baileys smak. Den desiderte bestselger innen smakstilsatt kaffe.";
            p.price = 69;
            p.category = "Aromafisert";
            p.stock = 50;
            p.imageURL=getRandomImage();
            return p;
        }
        /***********************************************************************************************************************/
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
            DBProduct.add(productModel);
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
        public static OrderModel createOrder()
        {
            var order = new OrderModel();
            var product = createAfterDinnerBlend();
            product.productID = 1;
            order.products.Add(product);
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