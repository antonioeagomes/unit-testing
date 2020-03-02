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
    public class HousekeeperServiceTests
    {
        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(h => h.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                new Housekeeper { Email = "a", FullName = "b", StatementEmailBody = "c", Oid = 1}
            }.AsQueryable);

            var statement = new Mock<IStatementGenerator>();
            var emailSender = new Mock<IEmailSender>();
            var messageBox = new Mock<IXtraMessageBox>();

            var service = new HousekeeperService(
                    uow.Object,
                    statement.Object,
                    emailSender.Object,
                    messageBox.Object);

            service.SendStatementEmails(new DateTime(2020, 1, 1));

            statement.Verify(s => s.SaveStatement(1, "b", new DateTime(2020, 1, 1)));
        }
    }
}
