using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(HotelRoom hotelRoom);
        Task<List<HotelRoom>> GetHotelRooms();
        // we need to specify something unique like primary key, but we don't have it 
        // due to this we will use composite keys to specify entity, to make it unique
        Task<HotelRoom> GetHotelRoom(int HotelId, int RoomNum);
        Task<HotelRoom> UpdateHotelRoom(int HotelId,int RoomNum, HotelRoom hotelRoom);
        Task Delete(int HotelId, int RoomNum);
    }
}
