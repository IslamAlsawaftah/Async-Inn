using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom hotelRoom)
        {
            _hotelRoom = hotelRoom;
        }

        // GET: api/HotelRoom/{hotelId}/Rooms
        [AllowAnonymous]
        [HttpGet("{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms()
        {
            return Ok(await _hotelRoom.GetHotelRooms());
        }

        // GET: api/HotelRoom/{hotelId}/Rooms/{roomNumber}
        [HttpGet("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int HotelID, int RoomNumber)
        {
            var hotelRoom = await _hotelRoom.GetHotelRoom(HotelID,RoomNumber);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        // PUT: api/HotelRoom/{hotelId}/Rooms/{roomNumber}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "DistrictManager")]
        [HttpPut("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int HotelID, int RoomNumber, HotelRoomDTO hotelRoom)
        {
            if (HotelID != hotelRoom.HotelID && RoomNumber != hotelRoom.RoomNumber)
            {
                return BadRequest();
            }
            var modifiedHotelRoom = await _hotelRoom.UpdateHotelRoom(HotelID, RoomNumber,hotelRoom);
            return Ok(modifiedHotelRoom);
        }

        // POST: api/HotelRoom/{hotelId}/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "DistrictManager")]
        [HttpPost("{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoomDTO hotelRoom)
        {
            HotelRoomDTO newHotelRoom = await _hotelRoom.Create(hotelRoom);
            return Ok(newHotelRoom);
        }

        // DELETE: api/HotelRoom/{hotelId}/Rooms/{roomNumber}
        [Authorize(Roles = "DistrictManager")]
        [HttpDelete("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int HotelID, int RoomNumber)
        {
            await _hotelRoom.Delete(HotelID, RoomNumber);
            return NoContent();
        }
    }
}
