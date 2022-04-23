namespace AsyncInn.Models
{
    // HotelRoom: join table
    public class HotelRoom
    {
        public int HotelId { get; set; }
        public int RoomNum { get; set; }
        public int RoomId { get; set; }
        public decimal Rate { get; set; }
        public bool IsPetFriendly { get; set; }

        // Navigation property
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
    }
}
//point of having join table: use linq to get the room have hotle room in hotel, room and hotel are connected
