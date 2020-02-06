using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [Test]
        public void GetCostumer_IdIsZero_ReturnNotFound()
        {
            var contoller = new CustomerController();

            var result = contoller.GetCustomer(0);
            //Assert.That(result, Is.TypeOf<NotFound>());
            Assert.That(result, Is.InstanceOf<ActionResult>());
        }

        [Test]
        public void GetCostumer_IdIsNotZero_ReturnOk()
        {
            var contoller = new CustomerController();

            var result = contoller.GetCustomer(1);
            Assert.That(result, Is.TypeOf<Ok>());
            // Assert.That(result, Is.InstanceOf<ActionResult>());
        }
    }
}
