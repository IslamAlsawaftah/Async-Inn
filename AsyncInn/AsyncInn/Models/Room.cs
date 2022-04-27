using System.Collections.Generic;

namespace AsyncInn.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Layout Layout { get; set; }
        // add new navigation property as a list because relation is one to many

        public List<HotelRoom> HotelRooms { get; set; }
        public List<RoomAmenity> RoomAmenity { get; set; }

    }
    public enum Layout
    {
        studio,
        onebedroom,
        twobedroom


    }
}
