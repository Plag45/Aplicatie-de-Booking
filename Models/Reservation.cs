namespace Aplicatie_de_Booking.Models
{
    public class Reservation
    {
        public Room_ID Room_ID { get; }
        public string Username { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public TimeSpan Lenght => EndTime.Subtract(StartTime);

        public Reservation(Room_ID room_ID, DateTime startTime, DateTime endTime, string username)
        {
            Room_ID = room_ID;
            StartTime = startTime;
            EndTime = endTime;
            Username = username;
        }

        public bool Conflicts(Reservation reservation)
        {
            if (reservation.Room_ID != Room_ID)
            {
                return false;

            }

            return reservation.StartTime < EndTime && reservation.EndTime > StartTime;
        }
    }
}
