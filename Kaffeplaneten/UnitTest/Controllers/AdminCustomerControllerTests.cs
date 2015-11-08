using Microsoft.VisualStudio.TestTools.UnitTesting;
using Administrasjon.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.BLL;
using Kaffeplaneten.Stubs;
using System.Web.Mvc;
using Kaffeplaneten.Models;
using System.Web;
using Moq;
using UnitTest;

namespace Administrasjon.Controllers.Tests
{
    [TestClass()]
    public class AdminCustomerControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            var controller = new AdminCustomerController(new CustomerBLL(new CustomerDALStub()), new LoggingBLL(new LoggingDALStub()));
            //Act
            var result = (ViewResult)controller.Index();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void AllCustomersTestOK()
        {
            //Arrange
            var controller = new AdminCustomerController(new CustomerBLL(new CustomerDALStub()), new LoggingBLL(new LoggingDALStub()));
            var list = new List<CustomerModel>();
            var customerModel = new CustomerModel();
            customerModel.customerID = 1;
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
            list.Add(customerModel);
            list.Add(customerModel);
            list.Add(customerModel);
            list.Add(customerModel);
            //Act
            var result = (ViewResult)controller.AllCustomers();
            var resultList = (List<CustomerModel>)result.Model;
            //Assert
            Assert.AreEqual(list.Count, resultList.Count);
            Assert.AreEqual(result. ViewName, "");
            for(int i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(resultList[i].adress, list[i].adress);
                Assert.AreEqual(resultList[i].customerID, list[i].customerID);
                Assert.AreEqual(resultList[i].email, list[i].email);
                Assert.AreEqual(resultList[i].firstName, list[i].firstName);
                Assert.AreEqual(resultList[i].lastName, list[i].lastName);
                Assert.AreEqual(resultList[i].password, list[i].password);
                Assert.AreEqual(resultList[i].payAdress, list[i].payAdress);
                Assert.AreEqual(resultList[i].payProvince, list[i].payProvince);
                Assert.AreEqual(resultList[i].payZipcode, list[i].payZipcode);
                Assert.AreEqual(resultList[i].phone, list[i].phone);
                Assert.AreEqual(resultList[i].sameAdresses, list[i].sameAdresses);
                Assert.AreEqual(resultList[i].zipCode, list[i].zipCode);
                Assert.AreEqual(resultList[i].province, list[i].province);
            }
        }


        [TestMethod()]
        public void EditTestOK()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminCustomerController(new CustomerBLL(new CustomerDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            var customerModel = new CustomerModel();
            customerModel.customerID = 1;
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
            
            //Act
            var result = (ViewResult)controller.Edit(1);
            var resultModel = (CustomerModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(resultModel.adress, customerModel.adress);
            Assert.AreEqual(resultModel.customerID, customerModel.customerID);
            Assert.AreEqual(resultModel.email, customerModel.email);
            Assert.AreEqual(resultModel.firstName, customerModel.firstName);
            Assert.AreEqual(resultModel.lastName, customerModel.lastName);
            Assert.AreEqual(resultModel.password, customerModel.password);
            Assert.AreEqual(resultModel.payAdress, customerModel.payAdress);
            Assert.AreEqual(resultModel.payProvince, customerModel.payProvince);
            Assert.AreEqual(resultModel.payZipcode, customerModel.payZipcode);
            Assert.AreEqual(resultModel.phone, customerModel.phone);
            Assert.AreEqual(resultModel.sameAdresses, customerModel.sameAdresses);
            Assert.AreEqual(resultModel.zipCode, customerModel.zipCode);
            Assert.AreEqual(resultModel.province, customerModel.province);
        }

        [TestMethod()]
        public void EditTestFalse()
        {
            //Arrange
            var controller = new AdminCustomerController(new CustomerBLL(new CustomerDALStub()), new LoggingBLL(new LoggingDALStub()));

            //Act
            var result = (ViewResult)controller.Edit(-1);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod()]
        public void EditTestPostTrue()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminCustomerController();
            var customerModel = new CustomerModel();
            customerModel.customerID = 1;
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
            //Act
            controller.Session["Employee"] = new EmployeeModel();
            controller.Session["tempCID"] = 1;
            var result = (RedirectToRouteResult)controller.Edit(customerModel);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllCustomers");
        }
        [TestMethod()]
        public void EditTestPostFalse()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminCustomerController();
            var customerModel = new CustomerModel();
            customerModel.customerID = -1;
            customerModel.firstName = "";
            customerModel.lastName = "Nordmann";
            customerModel.payAdress = "Osloveien 1";
            customerModel.payProvince = "Oslo";
            customerModel.payZipcode = "1234";
            customerModel.phone = "12345678";
            customerModel.province = "Oslo";
            customerModel.sameAdresses = true;
            customerModel.zipCode = "1234";
            customerModel.adress = "Osloveien 1";
            //Act
            controller.Session["tempCID"] = 1;
            controller.Session["Employee"] = new EmployeeModel();
            var result = (ViewResult)controller.Edit(customerModel);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void DeleteTestTrue()
        {
            //Arrange
            var controller = new AdminCustomerController(new CustomerBLL(new CustomerDALStub()), new LoggingBLL(new LoggingDALStub()));

            //Act
            var result = (RedirectToRouteResult)controller.Delete(1);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllCustomers");
        }
        [TestMethod()]
        public void DeleteTestFalse()
        {
            //Arrange
            var controller = new AdminCustomerController(new CustomerBLL(new CustomerDALStub()), new LoggingBLL(new LoggingDALStub()));

            //Act
            var result = (ViewResult)controller.Delete(-1);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void DetailsTestTrue()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminCustomerController(new CustomerBLL(new CustomerDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            var customerModel = new CustomerModel();
            customerModel.customerID = 1;
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
            //Act
            var result = (ViewResult)controller.Details(1);
            var resultModel = (CustomerModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(resultModel.adress, customerModel.adress);
            Assert.AreEqual(resultModel.customerID, customerModel.customerID);
            Assert.AreEqual(resultModel.email, customerModel.email);
            Assert.AreEqual(resultModel.firstName, customerModel.firstName);
            Assert.AreEqual(resultModel.lastName, customerModel.lastName);
            Assert.AreEqual(resultModel.password, customerModel.password);
            Assert.AreEqual(resultModel.payAdress, customerModel.payAdress);
            Assert.AreEqual(resultModel.payProvince, customerModel.payProvince);
            Assert.AreEqual(resultModel.payZipcode, customerModel.payZipcode);
            Assert.AreEqual(resultModel.phone, customerModel.phone);
            Assert.AreEqual(resultModel.sameAdresses, customerModel.sameAdresses);
            Assert.AreEqual(resultModel.zipCode, customerModel.zipCode);
            Assert.AreEqual(resultModel.province, customerModel.province);
        }
        [TestMethod()]
        public void DetailsTestFalse()
        {
            //Arrange
            var controller = new AdminCustomerController(new CustomerBLL(new CustomerDALStub()), new LoggingBLL(new LoggingDALStub()));

            //Act
            var result = (RedirectToRouteResult)controller.Details(-1);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllCustomers");
        }
    }
}