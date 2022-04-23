namespace AsyncInn.Models
{
    public class RoomAmenity
    {
        public int AmenetiesID { get; set; }
        public int RoomID { get; set; }

        //Navigation property
        public Amenity Amenity { get; set; }
        public Room Room { get; set; }

    }
}
