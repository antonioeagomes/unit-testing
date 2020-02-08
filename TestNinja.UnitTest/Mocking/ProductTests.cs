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
    public class ProductTests
    {
        [Test]
        public void GetPrice_CustomerIsGold_ReturnPriceWithDiscount()
        {
            //arrange
            var product = new Product { ListPrice = 100 };

            //act
            var result = product.GetPrice(new Customer { IsGold = true });

            //assert
            Assert.That(result, Is.EqualTo(70));

        }

        [Test]
        public void GetPrice_GoldCostumer_ApplyDiscount()
        {
            var mockCustomer = new Mock<ICustomer>();
            mockCustomer.Setup(c => c.IsGold).Returns(true);

            var result = new Product { ListPrice = 100 }.GetPrice(mockCustomer.Object);

            Assert.That(result, Is.EqualTo(70));
        }
    }
}
