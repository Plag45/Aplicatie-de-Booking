using Aplicatie_de_Booking.Models;
using Aplicatie_de_Booking.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Aplicatie_de_Booking.Tests
{
    [TestFixture]
    public class LoginTests
    {
        private LoginViewModel _loginViewModel;
        private Mock<IUserRepository> _userRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loginViewModel = new LoginViewModel();
        }

        [Test]
        public void CanExecuteLoginCommand_ValidData_ReturnsTrue()
        {
            // Arrange
            _loginViewModel.Username = "validUser";
            _loginViewModel.Password = new SecureString();
            _loginViewModel.Password.AppendChar('p');
            _loginViewModel.Password.AppendChar('a');
            _loginViewModel.Password.AppendChar('s');
            _loginViewModel.Password.MakeReadOnly();

            // Act
            var result = _loginViewModel.LoginCommand.CanExecute(null);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanExecuteLoginCommand_InvalidData_ReturnsFalse()
        {
            // Arrange
            _loginViewModel.Username = "us";
            _loginViewModel.Password = new SecureString();
            _loginViewModel.Password.AppendChar('p');
            _loginViewModel.Password.AppendChar('a');
            _loginViewModel.Password.MakeReadOnly();

            // Act
            var result = _loginViewModel.LoginCommand.CanExecute(null);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ExecuteLoginCommand_ValidUser_SetsPrincipalAndHidesView()
        {
            // Arrange
            _loginViewModel.Username = "validUser";
            _loginViewModel.Password = new SecureString();
            _loginViewModel.Password.AppendChar('p');
            _loginViewModel.Password.AppendChar('a');
            _loginViewModel.Password.AppendChar('s');
            _loginViewModel.Password.MakeReadOnly();

            _userRepositoryMock
                .Setup(repo => repo.AuthenticateUser(It.IsAny<NetworkCredential>()))
                .Returns(true);

            // Act
            _loginViewModel.LoginCommand.Execute(null);

            // Assert
            Assert.That(_loginViewModel.IsViewVisible, Is.False);
            Assert.That(Thread.CurrentPrincipal.Identity.Name, Is.EqualTo("validUser"));
        }

        [Test]
        public void ExecuteLoginCommand_InvalidUser_SetsErrorMessage()
        {
            // Arrange
            _loginViewModel.Username = "invalidUser";
            _loginViewModel.Password = new SecureString();
            _loginViewModel.Password.AppendChar('p');
            _loginViewModel.Password.AppendChar('a');
            _loginViewModel.Password.AppendChar('s');
            _loginViewModel.Password.MakeReadOnly();

            _userRepositoryMock
                .Setup(repo => repo.AuthenticateUser(It.IsAny<NetworkCredential>()))
                .Returns(false);

            // Act
            _loginViewModel.LoginCommand.Execute(null);

            // Assert
            Assert.That(_loginViewModel.IsViewVisible, Is.False);
            Assert.That(_loginViewModel.ErrorMessage, Is.EqualTo("* Invalid username or password"));
        }
    }
}
