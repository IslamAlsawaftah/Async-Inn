using System.Collections.Generic;

namespace AsyncInn.Models.DTO
{
    public class RoomDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Layout { get; set; }
        public List<AmenityDTO> Amenities { get; set; }
    }
}
