using System.Collections.Generic;

namespace AsyncInn.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // add new navigation property as a list because relation is one to many

        public List<RoomAmenity> RoomAmenity { get; set; }

    }
}
