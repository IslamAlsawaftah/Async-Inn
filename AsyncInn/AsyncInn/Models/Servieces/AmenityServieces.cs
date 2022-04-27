using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Models.Servieces
    

{
        public class AmenityServieces : IAmenity
        {
            private readonly AsyncInnDbContext _context;

            public AmenityServieces(AsyncInnDbContext context)
            {
                _context = context;
            }
            public async Task<Amenity> Create(Amenity amenity)
            {
                _context.Entry(amenity).State = EntityState.Added;
                await _context.SaveChangesAsync();
                return amenity;
            }

            public async Task<Amenity> GetAmenity(int id)
            {
            // populate the navigation property details within the return object.
            Amenity amenity = await _context.Amenities.FindAsync(id);

            return amenity;
        }

            public async Task<List<Amenity>> GetAmenities()
            {
            // populate the navigation property details within the return object.
            var amenities = await _context.Amenities.ToListAsync();

            return amenities;
        }

            public async Task<Amenity> UpdateAmenity(int id, Amenity amenity)
            {
                _context.Entry(amenity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return amenity;
            }
            public async Task Delete(int id)
            {
                Amenity amenity = await GetAmenity(id);
                _context.Entry(amenity).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }
    }