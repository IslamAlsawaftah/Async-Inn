using AsyncInn.Data;
using AsyncInn.Models.DTO;
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

        public async Task<HotelDTO> GetHotel(int id)
        {
            return await _context.Hotels

              .Select(hotel => new HotelDTO
              {
                  ID = id,
                  Name = hotel.Name,
                  StreetAddress = hotel.StreetAddress,
                  City = hotel.City,
                  State = hotel.State,
                  Phone = hotel.Phone,
                  Rooms = hotel.HotelRooms
                  .Select(r => new HotelRoomDTO
                  {
                      HotelID = r.Hotel.Id,
                      RoomNumber = r.RoomNum,
                      Rate = r.Rate,
                      PetFriendly = r.IsPetFriendly,
                      RoomID = r.RoomId,
                      Room = r.Room.HotelRooms
                      .Select(r => new RoomDTO
                      {
                          ID = r.Room.Id,
                          Name = r.Room.Name,
                          Layout = (int)r.Room.Layout,
                          Amenities = r.Room.RoomAmenity
                         .Select(amenity => new AmenityDTO
                         {
                             ID = id,
                             Name = amenity.Room.Name,
                         }).ToList()
                      }).FirstOrDefault()
                  }).ToList()
              }).FirstOrDefaultAsync(a => a.ID == id);
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            return await _context.Hotels

                .Select(hotel => new HotelDTO
                {
                    ID = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Rooms = hotel.HotelRooms
                    .Select(r => new HotelRoomDTO
                    {
                        HotelID = r.Hotel.Id,
                        RoomNumber = r.RoomNum,
                        Rate = r.Rate,
                        PetFriendly = r.IsPetFriendly,
                        RoomID = r.RoomId,
                        Room = r.Room.HotelRooms
                        .Select(r => new RoomDTO
                        {
                            ID = r.Room.Id,
                            Name = r.Room.Name,
                            Layout = (int)r.Room.Layout,
                            Amenities = r.Room.RoomAmenity
                           .Select(amenity => new AmenityDTO
                           {
                               ID = amenity.Amenity.Id,
                               Name = amenity.Amenity.Name,
                           }).ToList()
                        }).FirstOrDefault()
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }
        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
    
}
