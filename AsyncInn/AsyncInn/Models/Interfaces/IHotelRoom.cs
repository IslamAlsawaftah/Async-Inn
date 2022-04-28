using AsyncInn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom);
        Task<List<HotelRoomDTO>> GetHotelRooms();
        // we need to specify something unique like primary key, but we don't have it 
        // due to this we will use composite keys to specify entity, to make it unique
        Task<HotelRoomDTO> GetHotelRoom(int HotelId, int RoomNum);
        Task<HotelRoomDTO> UpdateHotelRoom(int HotelId,int RoomNum, HotelRoomDTO hotelRoom);
        Task Delete(int HotelId, int RoomNum);
       // Task<HotelRoom> AddRoomToHotel(int id ,HotelRoom hotelRoom);
    }
}
