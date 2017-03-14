using FFY.Data;
using FFY.Data.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Data.EfRepositoryTests
{
    [TestFixture]
    public class GetById
    {
        [TestCase(42)]
        [TestCase(6)]
        public void ShouldCallFindMethodOfDbSet_WhenValidIdIsPassed(int id)
        {
            //Arrange
            var mockedDbSet = new Mock<IDbSet<MockedModel>>();
            mockedDbSet.Setup(s => s.Find(It.IsAny<int>())).Verifiable();

            var mockedContext = new Mock<IFFYDbContext>();
            mockedContext.Setup(c => c.Set<MockedModel>())
                .Returns(mockedDbSet.Object);

            var efRepository = new EfRepository<MockedModel>(mockedContext.Object);

            // Act
            var entity = efRepository.GetById(id);

            // Act and Assert
            mockedDbSet.Verify(s => s.Find(id), Times.Once);
        }

        [TestCase(42, "Elon")]
        [TestCase(6, "Matt")]
        public void ShouldReturnExactEntity_WhenValidIdIsPassed(int id, string name)
        {
            //Arrange
            var mockedModel = new MockedModel() { Id = id, Name = name };

            var mockedDbSet = new Mock<IDbSet<MockedModel>>();
            mockedDbSet.Setup(s => s.Find(id)).Returns(mockedModel);

            var mockedContext = new Mock<IFFYDbContext>();
            mockedContext.Setup(c => c.Set<MockedModel>())
                .Returns(mockedDbSet.Object);

            var efRepository = new EfRepository<MockedModel>(mockedContext.Object);

            // Act
            var entity = efRepository.GetById(id);

            // Act and Assert
            Assert.AreSame(mockedModel, entity);
        }
    }
}
