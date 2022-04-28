using AsyncInn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<AmenityDTO> Create(AmenityDTO amenity);
        Task<List<AmenityDTO>> GetAmenities();
        Task<AmenityDTO> GetAmenity(int id);
        Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenity);
        Task Delete(int id);
    }
}
