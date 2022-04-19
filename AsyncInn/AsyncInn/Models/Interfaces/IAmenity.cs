using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<Amenity> Create(Amenity amenity);
        Task<List<Amenity>> GetAmenities();
        Task<Amenity> GetAmenity(int id);
        Task<Amenity> UpdateAmenity(int id, Amenity hotel);
        Task Delete(int id);
    }
}
