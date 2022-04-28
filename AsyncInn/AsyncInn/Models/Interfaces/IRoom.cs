using AsyncInn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IRoom
    {
        Task<RoomDTO> Create(RoomDTO room);
        Task<List<RoomDTO>> GetRooms();
        Task<RoomDTO> GetRoom(int id);
        Task<RoomDTO> UpdateRoom(int id, RoomDTO room);
        Task Delete(int id);
        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmenityFromRoom(int roomId, int amenityId);
    }
}
