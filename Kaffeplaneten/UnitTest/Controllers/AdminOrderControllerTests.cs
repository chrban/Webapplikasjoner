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

namespace Administrasjon.Controllers.Tests
{
    [TestClass()]
    public class AdminOrderControllerTests
    {
        [TestMethod()]
        public void AllOrdersTest()
        {
            //Arrange
            var controller = new AdminOrderController(new OrderBLL(new OrderDALStub()), new LoggingBLL(new LoggingDALStub()));
            var list = new List<OrderModel>();
            var orderModel = new OrderModel();
            orderModel.customerID = 1;
            orderModel.orderNr = 1;
            orderModel.total = 100;
            list.Add(orderModel);
            list.Add(orderModel);
            list.Add(orderModel);
            list.Add(orderModel);
            //Act
            var result = (ViewResult)controller.AllOrders();
            var resultList = (List<OrderModel>)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(resultList.Count, list.Count);
            for (int i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(resultList[i].customerID, list[i].customerID);
                Assert.AreEqual(resultList[i].orderNr, list[i].orderNr);
                Assert.AreEqual(resultList[i].total, list[i].total);
            }
        }
        [TestMethod()]
        public void cancelOrderTestFalse()
        {
            //Arrange
            var controller = new AdminOrderController(new OrderBLL(new OrderDALStub()), new LoggingBLL(new LoggingDALStub()));
            //Act
            var result = (RedirectToRouteResult)controller.cancelOrder(-1);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllOrders");

        }
        [TestMethod()]
        public void cancelOrderTestTrue()
        {
            //Arrange
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controller = new AdminOrderController(new OrderBLL(new OrderDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            //Act
            var result = (RedirectToRouteResult)controller.cancelOrder(1);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllOrders");
        }

        [TestMethod()]
        public void findOrdersTestTrue()
        {
            //Arrange
            var controller = new AdminOrderController(new OrderBLL(new OrderDALStub()), new LoggingBLL(new LoggingDALStub()));
            var list = new List<OrderModel>();
            var orderModel = new OrderModel();
            orderModel.customerID = 1;
            orderModel.orderNr = 1;
            orderModel.total = 100;
            list.Add(orderModel);
            list.Add(orderModel);
            list.Add(orderModel);
            list.Add(orderModel);
            //Act
            var result = (ViewResult)controller.findOrders(1);
            var resultList = (List<OrderModel>)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "HelpView");
            Assert.AreEqual(resultList.Count, list.Count);
            for (int i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(resultList[i].customerID, list[i].customerID);
                Assert.AreEqual(resultList[i].orderNr, list[i].orderNr);
                Assert.AreEqual(resultList[i].total, list[i].total);
            }
        }
        [TestMethod()]
        public void findOrdersTestFalse()
        {
            //Arrange
            var controller = new AdminOrderController(new OrderBLL(new OrderDALStub()), new LoggingBLL(new LoggingDALStub()));
            //Act
            var result = (RedirectToRouteResult)controller.findOrders(-1);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllCustomers");
        }
    }
}