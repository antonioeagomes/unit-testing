using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest.Fundamentals
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseStringWithStrongTag()
        {
            /* Testando String */
            var formater = new HtmlFormatter();
            var result = formater.FormatAsBold("abc");
            // specific
            Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

            //more general
            /*
            * Assert.That(result, Does.StartWith("<strong>"));
            * Assert.That(result, Does.EndWith("</strong>"));
            * Assert.That(result, Does.Contain("abc"));
            */
        }
    }
}
