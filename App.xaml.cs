using Aplicatie_de_Booking.Models;
using System.Windows;

namespace Aplicatie_de_Booking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            Hotel hotel = new Hotel("Steaua Albastra");

            hotel.MakeReservations(new Reservation
                (
                new Room_ID(1, 3),
                new DateTime(2023, 3, 20),
                new DateTime(2023, 4, 3),
                "Mirel"
                ));
            hotel.MakeReservations(new Reservation
                (
                new Room_ID(1, 3),
                new DateTime(2023, 5, 20),
                new DateTime(2023, 6, 30),
                "Mirel"
                ));

            IEnumerable<Reservation> reservations = hotel.GetReservationsForUser("Mirel");
            base.OnStartup(e);

        }
    }

}
