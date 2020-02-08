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
    public class OrderServiceTest
    {
        [Test]
        public void PlaceOrder_WhenCalled_ShouldStoreOrder()
        {
            var mockStored = new Mock<IStorage>();
            var service = new OrderService(mockStored.Object);

            var order = new Order();
            service.PlaceOrder(order);
            mockStored.Verify(s => s.Store(order));
            

        }

    }
}
