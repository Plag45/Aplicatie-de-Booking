namespace Aplicatie_de_Booking.Models
{
    public class Room_ID
    {
        public Room_ID(int floorNumber, int roomNumber)
        {
            FloorNumber = floorNumber;
            RoomNumber = roomNumber;
        }

        public override string ToString()
        {
            return $"{FloorNumber}{RoomNumber}";
        }

        public int FloorNumber { get; }
        public int RoomNumber { get; }

        public override bool Equals(object? obj)
        {
            return obj is Room_ID room_ID &&
            FloorNumber == room_ID.FloorNumber &&
            RoomNumber == room_ID.RoomNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }
        public static bool operator ==(Room_ID roomID1, Room_ID roomID2)
        {
            if (roomID1 is null && roomID2 is null)
            {
                return true;
            }
            return !(roomID1 is null) && roomID1.Equals(roomID2);
        }
        public static bool operator !=(Room_ID roomID1, Room_ID roomID2)
        {
            return !(roomID1 == roomID2);
        }
    }

}
