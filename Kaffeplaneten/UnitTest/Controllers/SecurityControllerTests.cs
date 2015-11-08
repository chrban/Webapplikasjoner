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
    public class SecurityControllerTests
    {
        [TestMethod()]
        public void LoginviewTestLoggedInn()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            controller.Session[SuperController.LOGGED_INN] = true;
            //Act
            var result = (ViewResult)controller.Loginview();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod()]
        public void LoginviewTestNotLoggedInn()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            controller.Session[SuperController.LOGGED_INN] = null;
            //Act
            var result = (ViewResult)controller.Loginview();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void LoginviewTestPostOK()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            var user = new UserModel();
            user.ID = 1;
            user.username = "Ola";
            //Act
            var result = (RedirectToRouteResult)controller.Loginview(user);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Home");
        }
        [TestMethod()]
        public void LoginviewTestPostWrongLogin()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            var user = new UserModel();
            user.ID = -1;
            //Act
            var result = (ViewResult)controller.Loginview(user);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod()]
        public void LoginviewTestPostWrongUser()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            var user = new UserModel();
            user.ID = 1;
            user.username = "@kaffeplaneten.no";
            //Act
            var result = (ViewResult)controller.Loginview(user);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void LoggedInTestTrue()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            controller.Session[SuperController.LOGGED_INN] = true;
            //Act
            var result = (ViewResult)controller.LoggedIn();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod()]
        public void LoggedInTestFalse()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            controller.Session[SuperController.LOGGED_INN] = false;
            //Act
            var result = (RedirectToRouteResult)controller.LoggedIn();
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "index");
        }

        [TestMethod()]
        public void LoggedOutTest()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            //Act
            var result = (RedirectToRouteResult)controller.LoggedOut();
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Loginview");
        }

        [TestMethod()]
        public void ForgotPasswordTestOK()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            var epost = "epost";
            var temp = new SuperController();
            //Act
            var result = controller.ForgotPassword(epost);
            //Assert
            Assert.AreNotEqual(result, "");

        }
        [TestMethod()]
        public void ForgotPasswordTestNotOK()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            var epost = "@kaffeplaneten.no";
            var temp = new SuperController();
            //Act
            var result = controller.ForgotPassword(epost);
            //Assert
            Assert.AreEqual(result, "NF");
        }
        [TestMethod()]
        public void ForgotPasswordTestNotFailed()
        {
            //Arrange
            var controller = MockHttpSession.getMoqSecurityController();
            var epost = "false";
            //Act
            var result = controller.ForgotPassword(epost);
            //Assert
            Assert.AreNotEqual(result, "");
        }
    }
}