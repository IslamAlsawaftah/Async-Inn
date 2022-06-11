using AsyncInn.Models;
using AsyncInn.Models.DTO;
using AsyncInn.Models.Servieces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AsyncInn_Test
{
    public class UnitTest1 : Mock
    {
        [Fact]
        public async Task Test()
        {
            // Arrange
            var amenity = await CreateAndSaveTestAmenity();
            var room = await CreateAndSaveTestRoom();
            var repository = new RoomServieces(_db);

            // Act
            await repository.AddAmenityToRoom(room.Id, amenity.Id);

            // Assert
            var actualRoom = await repository.GetRoom(room.Id);

            Assert.Contains(actualRoom.Amenities, a => a.ID == amenity.Id);

            // Act
            await repository.RemoveAmenityFromRoom(room.Id, amenity.Id);

            // Assert
            actualRoom = await repository.GetRoom(room.Id);
            Assert.DoesNotContain(actualRoom.Amenities, a => a.ID == amenity.Id);

        }

        protected async Task<Room> CreateAndSaveTestRoom()
        {
            var room = new Room { Name = "TestRoom", Layout = 0 };

            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, room.Id); // Sanity check
            return room;
        }
        protected async Task<Amenity> CreateAndSaveTestAmenity()
        {
            var amenity = new Amenity { Name = "mini bar" };
            _db.Amenities.Add(amenity);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, amenity.Id); // Sanity check
            return amenity;
        }
        [Fact]
        public async void GetRooms()
        {
            var service = new RoomServieces(_db);
            List<RoomDTO> rooms = await service.GetRooms();
            Assert.Equal(3, rooms.Count); // becuase we have 6 category in seed data
        }
        [Fact]
        public async void GetAmenities()
        {
            var service = new AmenityServieces(_db);
            List<AmenityDTO> amenity = await service.GetAmenities();
            Assert.Equal(3, amenity.Count); // becuase we have 6 category in seed data
        }
        [Fact]
        public void TestGetRoom()
        {
            var RoomServieces = new RoomServieces(_db);
            RoomDTO roomDTO = new RoomDTO
            {
                Name = "one bedroom",
                Layout = ""
            };
            var Room = RoomServieces.GetRoom(1).Result;
            Assert.NotEqual("", Room.Name);
        }
        [Fact]
        public void TestGetAmenity()
        {
            var AmenityServieces = new AmenityServieces(_db);
            AmenityDTO amenityDTO = new AmenityDTO
            {
                ID = 1,
                Name = "air condition",
            };
            var Amenity = AmenityServieces.GetAmenity(1).Result;
            Assert.NotEqual("air condition", Amenity.Name);
        }
        [Fact]
        public async void TestUpdateRoom()
        {
            var RoomService = new RoomServieces(_db);

            RoomDTO roomDTO = new RoomDTO
            {
                Name = "one bedroom",
                Layout = ""
            };


            var room = await RoomService.UpdateRoom(1, roomDTO);
            var room1 = RoomService.GetRoom(1).Result;
            Assert.NotEqual("", room.Name);
        }
        [Fact]
        public async void TestUpdateAmenity()
        {
            var RoomService = new AmenityServieces(_db);

            AmenityDTO amenityDTO = new AmenityDTO
            {
                ID = 1,
                Name = "air condition",
            };
            var updatedroom = await RoomService.UpdateAmenity(1, amenityDTO);
            Assert.NotEqual("mini bar", updatedroom.Name);
        }
        [Fact]
        public async void TestDeleteRoom()
        {
            var RoomService = new RoomServieces(_db);
            await RoomService.Delete(1);
            var res = await RoomService.GetRoom(1);
            Assert.Null(res);
        }
        [Fact]
        public async void TestDeleteAmenity()
        {
            var AmenityServieces = new AmenityServieces(_db);
            await AmenityServieces.Delete(2);
            var res = await AmenityServieces.GetAmenities();
            Assert.Equal(2,res.Count);
        }

    }
}
