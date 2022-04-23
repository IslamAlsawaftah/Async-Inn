using System.Collections.Generic;

namespace AsyncInn.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        // add new navigation property as a list because relation is one to many

        public List<HotelRoom> HotelRooms { get; set; }
    }
}
