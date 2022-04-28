using AsyncInn.Data;
using AsyncInn.Models.DTO;
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
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom)
        {
            HotelRoom hotelRoom1 = new HotelRoom
            {
                HotelId = hotelRoom.HotelID,
                RoomNum = hotelRoom.RoomNumber,
                RoomId = hotelRoom.RoomID,
                Rate = hotelRoom.Rate,
                IsPetFriendly = hotelRoom.PetFriendly
            };
            _context.Entry(hotelRoom1).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
        public async Task<HotelRoomDTO> GetHotelRoom(int HotelId, int RoomNum)
        {
            return await _context.HotelRooms
                                 .Select(hrdt => new HotelRoomDTO()
                                 {
                                     HotelID = hrdt.HotelId,
                                     RoomNumber = hrdt.RoomNum,
                                     Rate = hrdt.Rate,
                                     PetFriendly = hrdt.IsPetFriendly,
                                     RoomID = hrdt.RoomId,
                                     Room =  new RoomDTO()
                                     {
                                         ID = hrdt.Room.Id,
                                         Name = hrdt.Room.Name,
                                         Layout = (int)hrdt.Room.Layout,
                                         Amenities = hrdt.Room.RoomAmenity
                                         .Select(amenity => new AmenityDTO
                                         {
                                             ID = amenity.Amenity.Id,
                                             Name = amenity.Amenity.Name,
                                         }).ToList()
                                     }
                                 }).FirstOrDefaultAsync(x => x.HotelID == HotelId && x.RoomNumber == RoomNum);

        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms()
        {
            return await _context.HotelRooms
                               .Select(hrdt => new HotelRoomDTO()
                               {
                                   HotelID = hrdt.HotelId,
                                   RoomNumber = hrdt.RoomNum,
                                   Rate = hrdt.Rate,
                                   PetFriendly = hrdt.IsPetFriendly,
                                   RoomID = hrdt.RoomId,
                                   Room = new RoomDTO()
                                   {
                                       ID = hrdt.Room.Id,
                                       Name = hrdt.Room.Name,
                                       Layout = (int)hrdt.Room.Layout,
                                       Amenities = hrdt.Room.RoomAmenity
                                       .Select(amenity => new AmenityDTO
                                       {
                                           ID = amenity.Amenity.Id,
                                           Name = amenity.Amenity.Name,
                                       }).ToList()
                                   }
                               }).ToListAsync();

        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int HotelId, int RoomNum, HotelRoomDTO hotelRoom)
        {
            HotelRoom hotelRoom1 = new HotelRoom
            {
                HotelId = hotelRoom.HotelID,
                RoomNum = hotelRoom.RoomNumber,
                RoomId = hotelRoom.RoomID,
                Rate = hotelRoom.Rate,
                IsPetFriendly = hotelRoom.PetFriendly
            };
            _context.Entry(hotelRoom1).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
        public async Task Delete(int HotelId, int RoomNum)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(HotelId, RoomNum);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
