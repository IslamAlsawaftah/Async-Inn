using AsyncInn.Models.Servieces;
using System;
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
    }
}
