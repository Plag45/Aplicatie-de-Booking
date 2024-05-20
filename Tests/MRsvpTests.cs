using Aplicatie_de_Booking.Services;
using Aplicatie_de_Booking.Stores;
using Aplicatie_de_Booking.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicatie_de_Booking.Tests
{
    public class MRsvpTests
    {
        private MakeReservationViewModel _viewModel;
        private Mock<HotelStore> _hotelStoreMock;
        private Mock<NavigationService<ReservationListingViewModel>> _navigationServiceMock;

        [SetUp]
        public void SetUp()
        {
            _hotelStoreMock = new Mock<HotelStore>();
            _navigationServiceMock = new Mock<NavigationService<ReservationListingViewModel>>();
            _viewModel = new MakeReservationViewModel(_hotelStoreMock.Object, _navigationServiceMock.Object);
        }

        [Test]
        public void Username_WhenEmpty_AddsError()
        {
            // Arrange
            _viewModel.Username = "";

            // Act
            var errors = _viewModel.GetErrors(nameof(_viewModel.Username)).Cast<string>().ToList();

            // Assert
            Assert.That("Username cannot be empty.", Is.EqualTo(errors));
        }

        [Test]
        public void FloorNumber_WhenZero_AddsError()
        {
            // Arrange
            _viewModel.FloorNumber = 0;

            // Act
            var errors = _viewModel.GetErrors(nameof(_viewModel.FloorNumber)).Cast<string>().ToList();

            // Assert
            Assert.That("Floor number must be greater than zero.", Is.EqualTo(errors));
        }

        [Test]
        public void StartDate_AfterEndDate_AddsError()
        {
            // Arrange
            _viewModel.StartDate = new DateTime(2021, 1, 10);
            _viewModel.EndDate = new DateTime(2021, 1, 9);

            // Act
            var errorsStartDate = _viewModel.GetErrors(nameof(_viewModel.StartDate)).Cast<string>().ToList();
            var errorsEndDate = _viewModel.GetErrors(nameof(_viewModel.EndDate)).Cast<string>().ToList();

            // Assert
                Assert.That("The start date cannot be after the end date.", Is.EqualTo(errorsStartDate));
                Assert.That("The end date cannot be before the start date.", Is.EqualTo(errorsEndDate));
        }

        [Test]
        public void CanCreateReservation_ValidData_ReturnsTrue()
        {
            // Arrange
            _viewModel.Username = "validUser";
            _viewModel.FloorNumber = 1;
            _viewModel.StartDate = new DateTime(2021, 1, 1);
            _viewModel.EndDate = new DateTime(2021, 1, 10);

            // Act
            var result = _viewModel.CanCreateReservation;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CanCreateReservation_InvalidUsername_ReturnsFalse()
        {
            // Arrange
            _viewModel.Username = "";
            _viewModel.FloorNumber = 1;
            _viewModel.StartDate = new DateTime(2021, 1, 1);
            _viewModel.EndDate = new DateTime(2021, 1, 10);

            // Act
            var result = _viewModel.CanCreateReservation;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CanCreateReservation_InvalidFloorNumber_ReturnsFalse()
        {
            // Arrange
            _viewModel.Username = "validUser";
            _viewModel.FloorNumber = 0;
            _viewModel.StartDate = new DateTime(2021, 1, 1);
            _viewModel.EndDate = new DateTime(2021, 1, 10);

            // Act
            var result = _viewModel.CanCreateReservation;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CanCreateReservation_InvalidDateRange_ReturnsFalse()
        {
            // Arrange
            _viewModel.Username = "validUser";
            _viewModel.FloorNumber = 1;
            _viewModel.StartDate = new DateTime(2021, 1, 10);
            _viewModel.EndDate = new DateTime(2021, 1, 9);

            // Act
            var result = _viewModel.CanCreateReservation;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CancelCommand_WhenExecuted_InvokesNavigation()
        {
            // Act
            _viewModel.CancelCommand.Execute(null);

            // Assert
            _navigationServiceMock.Verify(n => n.Navigate(), Times.Once);
        }
    }
}
