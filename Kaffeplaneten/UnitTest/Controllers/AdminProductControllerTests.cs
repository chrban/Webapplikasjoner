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
    public class AdminProductControllerTests
    {
        [TestMethod()]
        public void AllProductsTestTrue()
        {
            //Arrange
            var controller = new AdminProductController(new ProductBLL(new ProductDALStub()), new LoggingBLL(new LoggingDALStub()));
            var list = new List<ProductModel>();
            var productModel = new ProductModel();
            productModel.category = "Kaffe";
            productModel.description = "God kaffe";
            productModel.imageURL = "kaffe.kaffebilde.jpg";
            productModel.price = 100;
            productModel.productID = 1;
            productModel.productName = "Svart kaffe";
            productModel.quantity = 10;
            productModel.stock = 100;
            list.Add(productModel);
            list.Add(productModel);
            list.Add(productModel);
            list.Add(productModel);
            //Act
            var result = (ViewResult)controller.AllProducts();
            var resultList = (List<ProductModel>)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(list.Count, resultList.Count);
            for(int i = 0; i < resultList.Count; i++)
            {
                Assert.AreEqual(resultList[i].category, list[i].category);
                Assert.AreEqual(resultList[i].description, list[i].description);
                Assert.AreEqual(resultList[i].imageURL, list[i].imageURL);
                Assert.AreEqual(resultList[i].price, list[i].price);
                Assert.AreEqual(resultList[i].productID, list[i].productID);
                Assert.AreEqual(resultList[i].productName, list[i].productName);
                Assert.AreEqual(resultList[i].quantity, list[i].quantity);
                Assert.AreEqual(resultList[i].stock, list[i].stock);
            }
        }

        [TestMethod()]
        public void EditTestFalse()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            //Act
            var result = (ViewResult)controller.Edit(-1);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod()]
        public void EditTestTrue()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            var productModel = new ProductModel();
            productModel.category = "Kaffe";
            productModel.description = "God kaffe";
            productModel.imageURL = "kaffe.kaffebilde.jpg";
            productModel.price = 100;
            productModel.productID = 1;
            productModel.productName = "Svart kaffe";
            productModel.quantity = 10;
            productModel.stock = 100;
            //Act
            var result = (ViewResult)controller.Edit(1);
            var resultModel = (ProductModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(productModel.category, resultModel.category);
            Assert.AreEqual(productModel.description, resultModel.description);
            Assert.AreEqual(productModel.imageURL, resultModel.imageURL);
            Assert.AreEqual(productModel.price, resultModel.price);
            Assert.AreEqual(productModel.productID, resultModel.productID);
            Assert.AreEqual(productModel.productName, resultModel.productName);
            Assert.AreEqual(productModel.stock, resultModel.stock);
            Assert.AreEqual(productModel.quantity, resultModel.quantity);
        }

        [TestMethod()]
        public void EditTestPostTrue()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            controller.Session["Employee"] = new EmployeeModel();
            controller.Session["tempPID"] = 1;
            var productModel = new ProductModel();
            productModel.category = "Kaffe";
            productModel.description = "God kaffe";
            productModel.imageURL = "kaffe.kaffebilde.jpg";
            productModel.price = 100;
            productModel.productID = 1;
            productModel.productName = "Svart kaffe";
            productModel.quantity = 10;
            productModel.stock = 100;
            //Act
            var result = (RedirectToRouteResult)controller.Edit(productModel);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllProducts");
        }
        [TestMethod()]
        public void EditTestPostFalse()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            controller.Session["Employee"] = new EmployeeModel();
            controller.Session["tempPID"] = 1;
            var productModel = new ProductModel();
            productModel.category = "Kaffe";
            productModel.description = "God kaffe";
            productModel.imageURL = "kaffe.kaffebilde.jpg";
            productModel.price = 100;
            productModel.productID = 1;
            productModel.productName = "";
            productModel.quantity = 10;
            productModel.stock = 100;
            //Act
            var result = (ViewResult)controller.Edit(productModel);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void DeleteTestTrue()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            controller.Session["Employee"] = new EmployeeModel();
            //Act
            var result = (RedirectToRouteResult)controller.Delete(1);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllProducts");
        }
        [TestMethod()]
        public void DeleteTest()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            //Act
            var result = (ViewResult)controller.Delete(-1);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void AddTest()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            //Act
            var result = (ViewResult)controller.Add();
            //Assert
            Assert.AreEqual(result.ViewName, "Add");
        }

        [TestMethod()]
        public void AddTestTrue()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            controller.Session["Employee"] = new EmployeeModel();
            var productModel = new ProductModel();
            productModel.category = "Kaffe";
            productModel.description = "God kaffe";
            productModel.imageURL = "kaffe.kaffebilde.jpg";
            productModel.price = 100;
            productModel.productID = 1;
            productModel.productName = "Svart kaffe";
            productModel.quantity = 10;
            productModel.stock = 100;
            //Act
            var result = (RedirectToRouteResult)controller.Add(productModel);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllProducts");
        }
        [TestMethod()]
        public void AddTestFalse()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            controller.Session["Employee"] = new EmployeeModel();
            var productModel = new ProductModel();
            productModel.category = "Kaffe";
            productModel.description = "God kaffe";
            productModel.imageURL = "kaffe.kaffebilde.jpg";
            productModel.price = 100;
            productModel.productID = 1;
            productModel.productName = "";
            productModel.quantity = 10;
            productModel.stock = 100;
            //Act
            var result = (ViewResult)controller.Add(productModel);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }

        [TestMethod()]
        public void DetailsTestTrue()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            var productModel = new ProductModel();
            productModel.category = "Kaffe";
            productModel.description = "God kaffe";
            productModel.imageURL = "kaffe.kaffebilde.jpg";
            productModel.price = 100;
            productModel.productID = 1;
            productModel.productName = "Svart kaffe";
            productModel.quantity = 10;
            productModel.stock = 100;
            //Act
            var result = (ViewResult)controller.Details(1);
            var resultModel = (ProductModel)result.Model;
            //Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(productModel.category, resultModel.category);
            Assert.AreEqual(productModel.description, resultModel.description);
            Assert.AreEqual(productModel.imageURL, resultModel.imageURL);
            Assert.AreEqual(productModel.price, resultModel.price);
            Assert.AreEqual(productModel.productID, resultModel.productID);
            Assert.AreEqual(productModel.productName, resultModel.productName);
            Assert.AreEqual(productModel.stock, resultModel.stock);
            Assert.AreEqual(productModel.quantity, resultModel.quantity);
        }
        [TestMethod()]
        public void DetailsTestFalse()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            //Act
            var result = (RedirectToRouteResult)controller.Details(-1);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "AllProducts");
        }

        [TestMethod()]
        public void UploaderTest()
        {
            //Arrange
            var controller = MockHttpSession.getMoqAdminProductController();
            //Act
            var result = (ViewResult)controller.Uploader();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
    }
}