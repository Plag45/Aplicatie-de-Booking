using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Aplicatie_de_Booking.ViewModels;
using Aplicatie_de_Booking.Views;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Aplicatie_de_Booking.Tests
{
    [TestFixture]
    public class BorderTests
    {
        private LoginView _loginView;

        [SetUp]
        public void SetUp()
        {
            _loginView = new LoginView();
        }

        [Test]
        public void TestBtnMinimizeClick_MinimizesWindow()
        {
            LoginView window = new LoginView();
            window.Show();

            var button = (Button)window.FindName("btnMinimize");
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            Assert.That(window.WindowState, Is.EqualTo(WindowState.Minimized));
        }

        [Test]
        public void TestBtnMinimizeClick_ButtonIsVisible()
        {
            LoginView window = new LoginView();
            window.Show();

            var button = (Button)window.FindName("btnMinimize");

            Assert.That(button.Visibility, Is.EqualTo(Visibility.Visible));
        }

        [Test]
        public void btnClose_Click_ShutsDownApplication()
        {
            // Arrange
            var button = new Button();
            var eventArgs = new RoutedEventArgs();

            // Act & Assert
            Assert.Throws<ApplicationException>(() => _loginView.btnClose_Click(button, eventArgs));
        }

       
    }
}
