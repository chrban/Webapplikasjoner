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

namespace Administrasjon.Controllers.Tests
{
    [TestClass()]
    public class AdminEmployeeControllerTests
    {
        [TestMethod()]
        public void AllEmployeesTest()
        {
            //Arrange
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            var list = new List<EmployeeModel>();
            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = 1;
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.customerAdmin = false;
            employeeModel.orderAdmin = true;
            employeeModel.databaseAdmin = false;
            employeeModel.employeeAdmin = false;
            employeeModel.password = "123456789";
            employeeModel.productAdmin = false;
            employeeModel.username = "Ola";
            list.Add(employeeModel);
            list.Add(employeeModel);
            list.Add(employeeModel);
            list.Add(employeeModel);
            //Act
            var result = (ViewResult)controller.AllEmployees();
            var resultList = (List<EmployeeModel>)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(resultList.Count, list.Count);
            for(int i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(resultList[i].customerAdmin, list[i].customerAdmin);
                Assert.AreEqual(resultList[i].databaseAdmin, list[i].databaseAdmin);
                Assert.AreEqual(resultList[i].employeeAdmin, list[i].employeeAdmin);
                Assert.AreEqual(resultList[i].employeeID, list[i].employeeID);
                Assert.AreEqual(resultList[i].firstName, list[i].firstName);
                Assert.AreEqual(resultList[i].lastName, list[i].lastName);
                Assert.AreEqual(resultList[i].orderAdmin, list[i].orderAdmin);
                Assert.AreEqual(resultList[i].password, list[i].password);
                Assert.AreEqual(resultList[i].phone, list[i].phone);
                Assert.AreEqual(resultList[i].productAdmin, list[i].productAdmin);
                Assert.AreEqual(resultList[i].username, list[i].username);
            }
        }

        [TestMethod()]
        public void createEmployeeTest()
        {
            //Arrange
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            //Act
            var result = (ViewResult)controller.createEmployee();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void createEmployeeTestFalseUsername()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = 1;
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.orderAdmin = true;
            employeeModel.customerAdmin = false;
            employeeModel.databaseAdmin = false;
            employeeModel.employeeAdmin = false;
            employeeModel.password = "123456789";
            employeeModel.productAdmin = false;
            employeeModel.username = "Ola";
            //Act
            var result = (ViewResult)controller.createEmployee(employeeModel);
            var resultModel = (EmployeeModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(employeeModel.firstName, resultModel.firstName);
            Assert.AreEqual(employeeModel.customerAdmin, resultModel.customerAdmin);
            Assert.AreEqual(employeeModel.databaseAdmin, resultModel.databaseAdmin);
            Assert.AreEqual(employeeModel.employeeAdmin, resultModel.employeeAdmin);
            Assert.AreEqual(employeeModel.employeeID, resultModel.employeeID);
            Assert.AreEqual(employeeModel.lastName, resultModel.lastName);
            Assert.AreEqual(employeeModel.orderAdmin, resultModel.orderAdmin);
            Assert.AreEqual(employeeModel.password, resultModel.password);
            Assert.AreEqual(employeeModel.phone, resultModel.phone);
            Assert.AreEqual(employeeModel.username, resultModel.username);

        }
        [TestMethod()]
        public void createEmployeeTestFalseName()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = 1;
            employeeModel.firstName = "";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.customerAdmin = false;
            employeeModel.orderAdmin = true;
            employeeModel.databaseAdmin = false;
            employeeModel.employeeAdmin = false;
            employeeModel.password = "123456789";
            employeeModel.productAdmin = false;
            employeeModel.username = "Ola";
            //Act
            var result = (ViewResult)controller.createEmployee(employeeModel);
            var resultModel = (EmployeeModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(employeeModel.firstName, resultModel.firstName);
            Assert.AreEqual(employeeModel.customerAdmin, resultModel.customerAdmin);
            Assert.AreEqual(employeeModel.databaseAdmin, resultModel.databaseAdmin);
            Assert.AreEqual(employeeModel.employeeAdmin, resultModel.employeeAdmin);
            Assert.AreEqual(employeeModel.employeeID, resultModel.employeeID);
            Assert.AreEqual(employeeModel.lastName, resultModel.lastName);
            Assert.AreEqual(employeeModel.orderAdmin, resultModel.orderAdmin);
            Assert.AreEqual(employeeModel.password, resultModel.password);
            Assert.AreEqual(employeeModel.phone, resultModel.phone);
            Assert.AreEqual(employeeModel.username, resultModel.username);
        }
        [TestMethod()]
        public void createEmployeeTestUserError()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = -1;
            employeeModel.firstName = "";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.customerAdmin = false;
            employeeModel.orderAdmin = true;
            employeeModel.databaseAdmin = false;
            employeeModel.employeeAdmin = false;
            employeeModel.password = "123456789";
            employeeModel.productAdmin = false;
            employeeModel.username = "Ola";
            //Act
            var result = (ViewResult)controller.createEmployee(employeeModel);
            var resultModel = (EmployeeModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(employeeModel.firstName, resultModel.firstName);
            Assert.AreEqual(employeeModel.customerAdmin, resultModel.customerAdmin);
            Assert.AreEqual(employeeModel.databaseAdmin, resultModel.databaseAdmin);
            Assert.AreEqual(employeeModel.employeeAdmin, resultModel.employeeAdmin);
            Assert.AreEqual(employeeModel.employeeID, resultModel.employeeID);
            Assert.AreEqual(employeeModel.lastName, resultModel.lastName);
            Assert.AreEqual(employeeModel.orderAdmin, resultModel.orderAdmin);
            Assert.AreEqual(employeeModel.password, resultModel.password);
            Assert.AreEqual(employeeModel.phone, resultModel.phone);
            Assert.AreEqual(employeeModel.username, resultModel.username);
        }
        [TestMethod()]
        public void createEmployeeTestTrue()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = 1;
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.customerAdmin = false;
            employeeModel.databaseAdmin = false;
            employeeModel.orderAdmin = true;
            employeeModel.employeeAdmin = false;
            employeeModel.password = "123456789";
            employeeModel.productAdmin = false;
            employeeModel.username = "";
            //Act
            var result = (RedirectToRouteResult)controller.createEmployee(employeeModel);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllEmployees");
        }

        [TestMethod()]
        public void deleteEmployeeTest()
        {
            //Arrange
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            //Act
            var result = (ViewResult)controller.deleteEmployee();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void deleteEmployeeTestPostNoUser()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = 1;
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.customerAdmin = false;
            employeeModel.databaseAdmin = false;
            employeeModel.employeeAdmin = false;
            employeeModel.password = "123456789";
            employeeModel.orderAdmin = true;
            employeeModel.productAdmin = false;
            employeeModel.username = "Ola";
            //Act
            var result = (ViewResult)controller.createEmployee(employeeModel);
            var resultModel = (EmployeeModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod()]
        public void deleteEmployeeTestPostSuperAdmin()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = 1;
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.customerAdmin = true;
            employeeModel.databaseAdmin = true;
            employeeModel.employeeAdmin = true;
            employeeModel.password = "123456789";
            employeeModel.productAdmin = true;
            employeeModel.orderAdmin = true;
            employeeModel.username = "";
            //Act
            var result = (ViewResult)controller.deleteEmployee(employeeModel);
            var resultModel = (EmployeeModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod()]
        public void deleteEmployeeTestPostDeleteError()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = -1;
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.customerAdmin = false;
            employeeModel.databaseAdmin = false;
            employeeModel.employeeAdmin = false;
            employeeModel.password = "123456789";
            employeeModel.orderAdmin = true;
            employeeModel.productAdmin = false;
            employeeModel.username = "";
            //Act
            var result = (ViewResult)controller.deleteEmployee(employeeModel);
            var resultModel = (EmployeeModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod()]
        public void deleteEmployeeTestPost()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;

            var employeeModel = new EmployeeModel();
            employeeModel.employeeID = 1;
            employeeModel.firstName = "Ola";
            employeeModel.lastName = "Nordmann";
            employeeModel.phone = "12345678";
            employeeModel.customerAdmin = false;
            employeeModel.databaseAdmin = false;
            employeeModel.employeeAdmin = false;
            employeeModel.password = "123456789";
            employeeModel.orderAdmin = true;
            employeeModel.productAdmin = false;
            employeeModel.username = "";

            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            //Act
            var result = (ViewResult)controller.deleteEmployee(employeeModel);
            var resultModel = (EmployeeModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
    }
}