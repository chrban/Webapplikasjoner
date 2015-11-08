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
using Newtonsoft.Json.Linq;

namespace Administrasjon.Controllers.Tests
{
    [TestClass()]
    public class AdminLoggingControllerTests
    {
        //Arrange

        //Act

        //Assert

        [TestMethod()]
        public void LoggingTest()
        {
            //Arrange
            var controller = new AdminLoggingController(new LoggingBLL(new LoggingDALStub()));
            //Act
            var result = (ViewResult)controller.Logging();
            //Assert
            Assert.AreEqual(result.ViewName, "Logging");
        }

        [TestMethod()]
        public void getInteractionMessagesTest()
        {
            //Arrange
            var controller = new AdminLoggingController(new LoggingBLL(new LoggingDALStub()));
            var array = new JArray();
            array.Add("LogData");
            //Act
            var result = controller.getInteractionMessages();
            //Assert
            Assert.AreEqual(result.Count, array.Count);
            for (int i = 0; i < result.Count; i++)
                Assert.AreEqual(result[i].ToString(), array[i].ToString());

        }

        [TestMethod()]
        public void getDatabaseMessagesTest()
        {
            //Arrange
            var controller = new AdminLoggingController(new LoggingBLL(new LoggingDALStub()));
            var array = new JArray();
            array.Add("LogData");
            //Act
            var result = controller.getDatabaseMessages();
            //Assert
            Assert.AreEqual(result.Count, array.Count);
            for (int i = 0; i < result.Count; i++)
                Assert.AreEqual(result[i].ToString(), array[i].ToString());
        }
    }
}