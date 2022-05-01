﻿using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Servieces
{

    public class HotelRoomServiece : IHotelRoom
    {
        private readonly AsyncInnDbContext _context;
        public HotelRoomServiece(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<HotelRoom> Create(int id ,HotelRoom hotelRoom)
        {
            hotelRoom.HotelId = id;
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
        public async Task<HotelRoom> GetHotelRoom(int HotelId, int RoomNum)
        {
            return await _context.HotelRooms.Include(x => x.Room)
                                                    .ThenInclude(x => x.RoomAmenity)
                                                    .ThenInclude(x => x.Amenity)
                                                    .Include(r => r.Hotel)
                                                    .ThenInclude(x => x.HotelRooms)
                                                    .FirstOrDefaultAsync(x => x.HotelId == HotelId && x.RoomNum == RoomNum);
        
        }

        public async Task<List<HotelRoom>> GetHotelRooms()
        {
            return await _context.HotelRooms.Include(hr => hr.Room)
                                            .ThenInclude(r => r.RoomAmenity)
                                            .ThenInclude(x => x.Amenity)
                                            .Include(hr => hr.Hotel)
                                            .ThenInclude(h => h.HotelRooms)
                                            .ToListAsync(); // list of hotelroom
        }

        public async Task<HotelRoom> UpdateHotelRoom(int HotelId, int RoomNum, HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
        public async Task Delete(int HotelId, int RoomNum)
        {
            HotelRoom hotelRoom = await GetHotelRoom(HotelId, RoomNum);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
