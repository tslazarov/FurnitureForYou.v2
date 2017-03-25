using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FFY.UnitTests.Services.RoomsServiceTests
{
    [TestFixture]
    public class GetRooms
    {
        [Test]
        public void ShouldCallAllMethodOfDataRoomsRepository()
        {
            // Arrange
            var rooms = new List<Room>() {
                new Room() { Name = "Kitchen" },
                new Room() { Name = "Bathroom" }
            }
            .AsQueryable();
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.RoomsRepository.All())
                .Returns(rooms)
                .Verifiable();

            var roomsService = new RoomsService(mockedData.Object);

            // Act
            roomsService.GetRooms();

            // Assert
            mockedData.Verify(d => d.RoomsRepository.All(), Times.Once);
        }

        [Test]
        public void ShouldReturnListWithAllRooms()
        {
            // Arrange
            var rooms = new List<Room>() {
                new Room() { Name = "Kitchen" },
                new Room() { Name = "Bathroom" }
            };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.RoomsRepository.All())
                .Returns(rooms.AsQueryable())
                .Verifiable();

            var roomsService = new RoomsService(mockedData.Object);

            // Act
            var result = roomsService.GetRooms();

            // Assert
            CollectionAssert.AreEqual(rooms, result);
        }
    }
}
