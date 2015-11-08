using Administrasjon.Controllers;
using Kaffeplaneten.BLL;
using Kaffeplaneten.Stubs;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UnitTest
{
    public class MockHttpSession:HttpSessionStateBase 
    {
        Dictionary<string, object> _sessionDictionary = new Dictionary<string, object>();
        public override object this[string name]
        {
            get { return _sessionDictionary[name]; }
            set { _sessionDictionary[name] = value; }
        }

        public static AdminCustomerController getMoqAdminCustomerController()
        {
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();
            context.Setup(m => m.HttpContext.Session).Returns(session);
            var controller = new AdminCustomerController(new CustomerBLL(new CustomerDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            return controller;
        }
        public static AdminEmployeeController getMoqAdminEmployeeController()
        {
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();
            context.Setup(m => m.HttpContext.Session).Returns(session);
            var controller = new AdminEmployeeController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            return controller;
        }
        public static AdminProductController getMoqAdminProductController()
        {
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();
            context.Setup(m => m.HttpContext.Session).Returns(session);
            var controller = new AdminProductController(new ProductBLL(new ProductDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            return controller;
        }
        public static LayoutController getMoqLayoutController()
        {
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();
            context.Setup(m => m.HttpContext.Session).Returns(session);
            var controller = new LayoutController();
            controller.ControllerContext = context.Object;
            return controller;
        }
        public static SecurityController getMoqSecurityController()
        {
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();
            context.Setup(m => m.HttpContext.Session).Returns(session);
            var controller = new SecurityController(new EmployeeBLL(new EmployeeDALStub()), new UserBLL(new UserDALStub()), new LoggingBLL(new LoggingDALStub()));
            controller.ControllerContext = context.Object;
            return controller;
        }
        public static SuperController getMoqSuperController()
        {
            var context = new Mock<ControllerContext>();
            var session = new MockHttpSession();
            context.Setup(m => m.HttpContext.Session).Returns(session);
            var controller = new SuperController();
            controller.ControllerContext = context.Object;
            return controller;
        }
    }
}
