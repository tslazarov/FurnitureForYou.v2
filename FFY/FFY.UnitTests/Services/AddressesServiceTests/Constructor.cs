﻿using FFY.Data.Contracts;
using FFY.Services;
using Moq;
using NUnit.Framework;
using System;

namespace FFY.UnitTests.Services.AddressesServiceTests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void ShouldThrowArgumentNullException_WhenNullDataIsPassed()
        {
            // Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() =>
                new AddressesService(null));
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWithCorrectMessage_WhenNullDataIsPassed()
        {
            // Arrange
            var expectedExMessage = "Data cannot be null.";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddressesService(null));
            StringAssert.Contains(expectedExMessage, exception.Message);
        }

        [Test]
        public void ShouldNotThrow_WhenValidDependenciesArePassed()
        {
            // Arrange
            var mockedData = new Mock<IFFYData>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
                new AddressesService(mockedData.Object));
        }
    }
}
