using AsyncInn.Data;
using AsyncInn.Models.DTO;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<RoomDTO> Create(RoomDTO room)
        {
            Room room1 = new Room
            {
                Id = room.ID,
                Name = room.Name,
                Layout = (Layout)room.Layout
            };
            _context.Entry(room1).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<RoomDTO> GetRoom(int id)
        {
            return await _context.Rooms

               .Select(room => new RoomDTO
               {
                   ID = id,
                   Name = room.Name,
                   Layout = (int)room.Layout,
                   Amenities = room.RoomAmenity
                    .Select(amenity => new AmenityDTO
                    {
                        ID = id,
                        Name = amenity.Room.Name,
                    }).ToList()
                    }).FirstOrDefaultAsync(a => a.ID == id);
        }


        public async Task<List<RoomDTO>> GetRooms()
        {
            return await _context.Rooms

                .Select(room => new RoomDTO
                {
                    ID = room.Id,
                    Name = room.Name,
                    Layout = (int)room.Layout,
                    Amenities = room.RoomAmenity
                     .Select(amenity => new AmenityDTO
                     {
                         ID = amenity.AmenityID,
                         Name = amenity.Room.Name,
                     }).ToList()
                }).ToListAsync();
        }

        public async Task<RoomDTO> UpdateRoom(int id, RoomDTO room)
        {
            Room room1 = new Room
            {
                Id = room.ID,
                Name = room.Name,
                Layout = (Layout)room.Layout
            };
            _context.Entry(room1).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }
        public async Task Delete(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity amenity = new RoomAmenity()
            {
                AmenityID = amenityId,
                RoomID = roomId
            };
            _context.Entry(amenity).State = EntityState.Added; // because we are creating a new one
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var removedAmenity = await _context.RoomAmenities.FirstOrDefaultAsync(i => i.RoomID == roomId && i.AmenityID == amenityId);
            _context.RoomAmenities.Remove(removedAmenity);
            //_context.Entry(removedAmenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
