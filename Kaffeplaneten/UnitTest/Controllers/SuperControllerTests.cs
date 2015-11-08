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
    public class SuperControllerTests
    {
        //Arrange
        
        //Act

        //Assert
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSuperController();
            //Act
            var result = (ViewResult)controller.Index();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void getActiveUserIDTestOK()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSuperController();
            var temp = new EmployeeModel();
            temp.employeeID = 1;
            controller.Session[SuperController.Employee] = temp;
            //Act
            var result = controller.getActiveUserID();
            //Assert
            Assert.AreEqual(result, 1);
        }
        [TestMethod()]
        public void getActiveUserIDTestNotOK()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSuperController();
            controller.Session[SuperController.Employee] = null;
            //Act
            var result = controller.getActiveUserID();
            //Assert
            Assert.AreEqual(result, -1);
        }

        [TestMethod()]
        public void getHashTest()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSuperController();
            var password = "123456789";
            //Act
            var result = controller.getHash(password);
            //Assert
            Assert.IsTrue(result.Length > 0);
        }
    }
}