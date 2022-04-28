using AsyncInn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotel
    {
        Task<Hotel> Create(Hotel hotel);
        Task<List<HotelDTO>> GetHotels();
        Task<HotelDTO> GetHotel(int id);
        Task<Hotel> UpdateHotel(int id, Hotel hotel);
        Task Delete(int id);
    }
}
