namespace Aplicatie_de_Booking.Models
{
    internal class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string Name { get; }

        public Hotel(string name)
        {
            Name = name;
            _reservationBook = new ReservationBook();
        }

        public IEnumerable<Reservation> GetReservationsForUser(string username)
        {
            return _reservationBook.GetReservationsForUser(username);
        }

        public void MakeReservations(Reservation reservation)
        {
            _reservationBook.AddReservation(reservation);

        }
    }
}
