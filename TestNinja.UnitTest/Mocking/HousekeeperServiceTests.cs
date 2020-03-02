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
        private string _fileName;
        private Mock<IUnitOfWork> _uow;
        private Housekeeper _housekeeper;
        private Mock<IStatementGenerator> _statement;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private HousekeeperService _service;
        private DateTime _statementDateTime = new DateTime(2020, 3, 2);

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", StatementEmailBody = "c", Oid = 1 };

            _uow = new Mock<IUnitOfWork>();
            _uow.Setup(h => h.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable);

            _statement = new Mock<IStatementGenerator>();
            _fileName = "fileName";
            _statement
                .Setup(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDateTime))
                .Returns(() => _fileName);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(
                    _uow.Object,
                    _statement.Object,
                    _emailSender.Object,
                    _messageBox.Object);
        }
        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDateTime);

            _statement.Verify(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDateTime));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_WhenEmailIsNullEmptyWhiteSpace_ShouldNotGenerateStatement(string email)
        {
            _housekeeper.Email = email;
            _service.SendStatementEmails(_statementDateTime);

            _statement.Verify(s => s.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDateTime), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailStatement()
        {
            _service.SendStatementEmails(_statementDateTime);
            VerifyEmailSent();

        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_WhenStatementFileNameIsNullEmptyOrWhiteSpace_ShouldNotEmail(string filename)
        {
            _fileName = filename;

            _service.SendStatementEmails(_statementDateTime);

            VerifyEmailNotSent();

        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            _emailSender.Setup(e => e.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                    )).Throws<Exception>();

            _service.SendStatementEmails(_statementDateTime);
            VerifyMessageBoxDisplayed();

        }

        private void VerifyMessageBoxDisplayed()
        {
            _messageBox.Verify(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(e => e.EmailFile(
                                It.IsAny<string>(),
                                It.IsAny<string>(),
                                It.IsAny<string>(),
                                It.IsAny<string>()),
                                Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(e => e.EmailFile(
                                _housekeeper.Email,
                                _housekeeper.StatementEmailBody,
                                _fileName,
                                It.IsAny<string>()));
        }
    }
}
