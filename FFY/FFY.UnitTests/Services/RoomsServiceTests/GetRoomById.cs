using FFY.Data.Contracts;
using FFY.Models;
using FFY.Services;
using Moq;
using NUnit.Framework;

namespace FFY.UnitTests.Services.RoomsServiceTests
{
    [TestFixture]
    public class GetRoomById
    {
        [TestCase(1, "Kitchen")]
        [TestCase(1, "Bedroom")]
        public void ShouldCallGetByIdMethodOfDataRoomsRepository(int id, string name)
        {
            // Arrange
            var room = new Room() { Id = id, Name = name };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.RoomsRepository.GetById(It.IsAny<int>()))
                .Returns(room)
                .Verifiable();

            var roomsService = new RoomsService(mockedData.Object);

            // Act
            roomsService.GetRoomById(id);

            // Assert
            mockedData.Verify(d => d.RoomsRepository.GetById(id), Times.Once);
        }

        [TestCase(1, "Bathroom")]
        [TestCase(1, "Living room")]
        public void ShouldReturnCorrectRoom(int id, string name)
        {
            // Arrange
            var room = new Room() { Id = id, Name = name };
            var mockedData = new Mock<IFFYData>();
            mockedData.Setup(d => d.RoomsRepository.GetById(It.IsAny<int>()))
                .Returns(room)
                .Verifiable();

            var roomsService = new RoomsService(mockedData.Object);

            // Act
            var result = roomsService.GetRoomById(id);

            // Assert
            Assert.AreEqual(room, result);
        }
    }
}
