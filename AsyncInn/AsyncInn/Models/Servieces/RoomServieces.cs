using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncInn.Models.Servieces
{
    public class RoomServieces : IRoom
    {
        private readonly AsyncInnDbContext _context;

        public RoomServieces(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<Room> Create(Room room)
        {
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> GetRoom(int id)
        {
            // populate the navigation property details within the return object.
            return await _context.Rooms
                                 .Include(a => a.RoomAmenity)
                                 .ThenInclude(b => b.Amenity)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Room>> GetRooms()
        {
            // populate the navigation property details within the return object.
            return await _context.Rooms
                               .Include(a => a.RoomAmenity)
                               .ThenInclude(b => b.Amenity)
                               .ToListAsync();
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }
        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity amenity = new RoomAmenity()
            {
                AmenetiesID = amenityId,
                RoomID = roomId
            };
            _context.Entry(amenity).State = EntityState.Added; // because we are creating a new one
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var removedAmenity = await _context.RoomAmenities.FirstOrDefaultAsync(i => i.RoomID == roomId && i.AmenetiesID == amenityId);
            _context.RoomAmenities.Remove(removedAmenity);
            //_context.Entry(removedAmenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
