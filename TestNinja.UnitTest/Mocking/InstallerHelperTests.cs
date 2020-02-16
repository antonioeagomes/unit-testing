using NUnit.Framework;
using Moq;
using TestNinja.Mocking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IDowloader> _mockDownloader;
        private InstallerHelper _installer;

        [SetUp]
        public void Init()
        {
            _mockDownloader = new Mock<IDowloader>();
            _installer = new InstallerHelper(_mockDownloader.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _mockDownloader = null;
            _installer = null;
        }

        [Test]
        public void DownloadInstaller_DownloadCompletes_ReturnTrue()
        {
            var result = _installer.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            /* _mockDownloader.Setup(d => d.Download("","")).Throws<WebException>(); */ // Test case tests only with these values "".
            /* Using Moq Framework */
            _mockDownloader.Setup(d => 
                d.Download(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            var result = _installer.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);
        }

    }
}
