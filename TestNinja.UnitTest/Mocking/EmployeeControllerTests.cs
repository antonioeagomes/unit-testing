using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb()
        {
            var mockStorage = new Mock<IEmployeeStorage>();
            var controller = new EmployeeController(mockStorage.Object);

            controller.DeleteEmployee(1);

            mockStorage.Verify(s => s.DeleteEmployee(1));
        }
    }
}
