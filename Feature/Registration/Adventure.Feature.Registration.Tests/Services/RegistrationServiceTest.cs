using System;
using Adventure.Feature.Registration.Services;
using Adventure.Foundation.Accounts.Providers.Interfaces;
using log4net;
using Moq;
using NUnit.Framework;

namespace Adventure.Feature.Registration.Tests.Services
{
    [TestFixture]
    public class RegistrationServiceTest
    {
        private const string UserName = "name";
        private const string Email = "email";
        private const string Password = "password";

        private RegistrationService _registrationService;
        private Mock<IAdventureAccountsProvider> _mockedAccountProvider;

        [SetUp]
        public void SetUp()
        {
            _mockedAccountProvider = new Mock<IAdventureAccountsProvider>();

            _registrationService = new RegistrationService(_mockedAccountProvider.Object, Mock.Of<ILog>());
        }

        [TestCase("")]
        [TestCase("  ")]
        [TestCase(null)]
        public void CreateUser_WhenUserNameNullOrWhiteSpace_ThrowAnArgumentException(string userName)
        {
            Assert.Throws<ArgumentException>(() => _registrationService.CreateUser(userName, Email, Password));

            _mockedAccountProvider.Verify(
                x => x.CreateUser(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }

        [TestCase("")]
        [TestCase("  ")]
        [TestCase(null)]
        public void CreateUser_WhenEmailNullOrWhiteSpace_ThrowAnArgumentException(string email)
        {
            Assert.Throws<ArgumentException>(() => _registrationService.CreateUser(UserName, email, Password));

            _mockedAccountProvider.Verify(
                x => x.CreateUser(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }

        [TestCase("")]
        [TestCase("  ")]
        [TestCase(null)]
        public void CreateUser_WhenPasswordNullOrWhiteSpace_ThrowAnArgumentException(string password)
        {
            Assert.Throws<ArgumentException>(() => _registrationService.CreateUser(UserName, Email, password));

            _mockedAccountProvider.Verify(
                x => x.CreateUser(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void CreateUser_WhenInputDataIsValid_CallCreateUserWithProperParameters()
        {
            var expected = true;
            _mockedAccountProvider
                .Setup(x => x.CreateUser(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(expected);

            var result = _registrationService.CreateUser(UserName, Email, Password);

            Assert.AreEqual(expected, result);
            _mockedAccountProvider.Verify(
                x => x.CreateUser(UserName, Email, Password),
                Times.Once);
        }

        [TestCase("")]
        [TestCase("  ")]
        [TestCase(null)]
        public void IsUserNameUnique_WhenUserNameIsNullOrWhiteSpace_ThrowAnArgumentException(string userName)
        {
            Assert.Throws<ArgumentException>(() => _registrationService.IsUserNameUnique(userName));
            _mockedAccountProvider.Verify(
                x => x.UserExists(It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void IsUserNameUnique_WhenUserNameValid_CallUserExistsAndReturnProperValue()
        {
            var expected = true;
            _mockedAccountProvider
                .Setup(x => x.UserExists(It.IsAny<string>()))
                .Returns(!expected);

            var result = _registrationService.IsUserNameUnique(UserName);

            Assert.AreEqual(expected, result);
            _mockedAccountProvider.Verify(x => x.UserExists(UserName), Times.Once);
        }
    }
}
