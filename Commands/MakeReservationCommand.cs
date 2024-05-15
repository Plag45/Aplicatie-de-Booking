using Aplicatie_de_Booking.Exceptions;
using Aplicatie_de_Booking.Models;
using Aplicatie_de_Booking.Services;
using Aplicatie_de_Booking.Stores;
using Aplicatie_de_Booking.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aplicatie_de_Booking.Commands
{
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly HotelStore _hotelStore;
        private MakeReservationViewModel makeReservationViewModel;
        private HotelStore hotelStore;
        private NavigationService<ReservationListingViewModel> reservationViewNavigationService;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel,
            HotelStore hotelStore,
            System.Windows.Navigation.NavigationService reservationViewNavigationService)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _hotelStore = hotelStore;

            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, HotelStore hotelStore, NavigationService<ReservationListingViewModel> reservationViewNavigationService)
        {
            this.makeReservationViewModel = makeReservationViewModel;
            this.hotelStore = hotelStore;
            this.reservationViewNavigationService = reservationViewNavigationService;
        }

        public override bool CanExecute(object parameter)
        {
            return _makeReservationViewModel.CanCreateReservation && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _makeReservationViewModel.SubmitErrorMessage = string.Empty;
            _makeReservationViewModel.IsSubmitting = true;

            Reservation reservation = new Reservation(
                new Room_ID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate,
                _makeReservationViewModel.Username);

            try
            {
                await _hotelStore.MakeReservation(reservation);

                MessageBox.Show("Successfully reserved room.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (ReservationConflictException)
            {
                _makeReservationViewModel.SubmitErrorMessage = "This room is already taken on those dates.";
            }
            catch (InvalidReservationTimeRangeException)
            {
                _makeReservationViewModel.SubmitErrorMessage = "Start date must be before end date.";
            }
            catch (Exception)
            {
                _makeReservationViewModel.SubmitErrorMessage = "Failed to make reservation.";
            }

            _makeReservationViewModel.IsSubmitting = false;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeReservationViewModel.CanCreateReservation))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
