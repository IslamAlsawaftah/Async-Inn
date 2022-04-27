using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Servieces
{
    public class HotelServices:IHotel
    {
        private readonly AsyncInnDbContext _context;

        public HotelServices(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> GetHotel(int id)
        {
            Hotel hotel = await _context.Hotels
                                        .Where(h => h.Id == id)
                                        .Include(h => h.HotelRooms)
                                        .ThenInclude(hr => hr.Room)
                                        .ThenInclude(ra => ra.RoomAmenity)
                                        .ThenInclude(a => a.Amenity)
                                        .FirstOrDefaultAsync();
   
            return hotel;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotels
                                       .Include(h => h.HotelRooms)
                                       .ThenInclude(hr => hr.Room)
                                       .ThenInclude(ra => ra.RoomAmenity)
                                       .ThenInclude(a => a.Amenity)
                                       .ToListAsync();
            return hotels;
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }
        public async Task Delete(int id)
        {
            Hotel hotel = await GetHotel(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
    
}
