using FFY.Data;
using FFY.Data.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Data.EfRepositoryTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullContextIsPassed()
        {
            // Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() => new EfRepository<MockedModel>(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullContextIsPassed()
        {
            // Arrange
            var expectedExMessage = "Database context cannot be null.";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            new EfRepository<MockedModel>(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }


        [Test]
        public void ShouldCallSetMethodOfContext_WhenValidContextIsPassed()
        {
            // Arrange
            var mockedContext = new Mock<IFFYDbContext>();

            // Act
            var genericRepository = new EfRepository<MockedModel>(mockedContext.Object);

            // Asert
            mockedContext.Verify(c => c.Set<MockedModel>(), Times.Once);
        }
    }
}
