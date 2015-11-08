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
using Moq;
using System.Web;
using UnitTest;

namespace Administrasjon.Controllers.Tests
{
    [TestClass()]
    public class LayoutControllerTests
    {        
        [TestMethod()]
        public void HeaderAndMenuBarTestLoggedInn()
        {
            //Arrange
            var controller = MockHttpSession.getMoqLayoutController();
            controller.Session["LoggedInn"] = true;
            var userModel = new UserModel();
            userModel.ID = 1;
            userModel.password = "123456789";
            userModel.username = "Ola@nordmann.no";
            //Act
            var result = (PartialViewResult)controller.HeaderAndMenuBar(userModel);
            var resultModel = (UserModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(resultModel.ID, userModel.ID);
            Assert.AreEqual(resultModel.password, userModel.password);
            Assert.AreEqual(resultModel.username, userModel.username);
        }

        [TestMethod()]
        public void EmployeeAdminBarTest()
        {
            //Arrange
            var controller = MockHttpSession.getMoqLayoutController();
            //Act
            var result = (PartialViewResult)controller.EmployeeAdminBar();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void HomeTest()
        {
            //Arrange
            var controller = MockHttpSession.getMoqLayoutController();
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
            employeeModel.username = "Ola";
            //Act
            var result = (ViewResult)controller.Home(employeeModel);
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
    }
}