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

namespace FFY.UnitTests.Data.DeletableEfRepositoryTests
{
    [TestFixture]
    public class AllWithoutDeleted
    {
        [Test]
        public void ShouldReturnCorrectAmountOfEntities()
        {
            //Arrange
            var mockedModel1 = new MockedModel() { IsDeleted = true };
            var mockedModel2 = new MockedModel() { IsDeleted = false };
            var mockedModel3 = new MockedModel() { IsDeleted = false };

            var data = new List<MockedModel>()
            {
                mockedModel1,
                mockedModel2,
                mockedModel3
            }.AsQueryable();

            var mockedDbSet = new Mock<IDbSet<MockedModel>>();
            mockedDbSet.As<IQueryable<MockedModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockedDbSet.As<IQueryable<MockedModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockedDbSet.As<IQueryable<MockedModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockedDbSet.As<IQueryable<MockedModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockedContext = new Mock<IFFYDbContext>();
            mockedContext.Setup(s => s.Set<MockedModel>())
                .Returns(mockedDbSet.Object);

            var deletableEfRepository = new DeletableEfRepository<MockedModel>(mockedContext.Object);

            // Act
            var all = deletableEfRepository.AllWithoutDeleted();

            // Assert
            Assert.AreEqual(2, all.Count());
        }

        [Test]
        public void ShouldReturnCorrectDbSetWithEntities()
        {
            //Arrange
            var mockedModel1 = new MockedModel() { IsDeleted = true };
            var mockedModel2 = new MockedModel() { IsDeleted = false };
            var mockedModel3 = new MockedModel() { IsDeleted = false };

            var data = new List<MockedModel>()
            {
                mockedModel1,
                mockedModel2,
                mockedModel3
            }.AsQueryable();

            var mockedDbSet = new Mock<IDbSet<MockedModel>>();
            mockedDbSet.As<IQueryable<MockedModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockedDbSet.As<IQueryable<MockedModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockedDbSet.As<IQueryable<MockedModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockedDbSet.As<IQueryable<MockedModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockedContext = new Mock<IFFYDbContext>();
            mockedContext.Setup(s => s.Set<MockedModel>())
                .Returns(mockedDbSet.Object);

            var deletableEfRepository = new DeletableEfRepository<MockedModel>(mockedContext.Object);

            // Act
            var all = deletableEfRepository.AllWithoutDeleted();

            // Assert
            Assert.AreSame(mockedModel2, all.First());
            Assert.AreSame(mockedModel3, all.Last());
        }
    }
}
