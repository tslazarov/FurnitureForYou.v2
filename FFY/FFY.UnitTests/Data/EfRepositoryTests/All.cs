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
    public class All
    {
        [Test]
        public void ShouldReturnDbSet()
        {
            //Arrange
            var mockedDbSet = new Mock<IDbSet<MockedModel>>();
            var mockedContext = new Mock<IFFYDbContext>();
            mockedContext.Setup(s => s.Set<MockedModel>())
                .Returns(mockedDbSet.Object);

            var efRepository = new EfRepository<MockedModel>(mockedContext.Object);

            // Act
            var all = efRepository.All(); 

            // Act and Assert
            Assert.AreSame(mockedDbSet.Object, all);
        }
    }
}
