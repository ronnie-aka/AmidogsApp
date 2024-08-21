using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Interfaces;
using AmidogsManager.BusinessLogic.Services;

namespace AmidogsManager.Tests.ServicesTest
{
    public class DogServiceTest
    {
        private readonly Mock<IDogRepository> dogRepositoryMock;
        private readonly Mock<IMatchRepository> matchRepositoryMock;
        private readonly Mock<IDogMeetingRepository> dogMeetingRepositoryMock;
        private readonly DogService dogService;

        public DogServiceTest()
        {
            dogRepositoryMock = new Mock<IDogRepository>();
            matchRepositoryMock = new Mock<IMatchRepository>();
            dogMeetingRepositoryMock = new Mock<IDogMeetingRepository>();
            dogService = new DogService(dogRepositoryMock.Object, matchRepositoryMock.Object, dogMeetingRepositoryMock.Object);
        }

        [Fact]
        public void GetDogByUser_ShouldReturnDog_WhenDogExists()
        {
            // Arrange
            int userId = 1;
            var expectedDog = new Dog { Id = 1, UserId = userId };
            dogRepositoryMock.Setup(repo => repo.GetByUser(userId)).Returns(expectedDog);

            // Act
            var result = dogService.GetDogByUser(userId);

            // Assert
            Assert.Equal(expectedDog, result);
        }

        [Fact]
        public void GetDogByUser_ShouldThrowException_WhenDogDoesNotExist()
        {
            // Arrange
            int userId = 1;
            dogRepositoryMock.Setup(repo => repo.GetByUser(userId)).Returns((Dog?)null);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => dogService.GetDogByUser(userId));
            Assert.Equal($"No dog found for user {userId}", exception.Message);
        }

        [Fact]
        public void GetUnmatchedDogs_ShouldReturnUnmatchedDogs()
        {
            // Arrange
            int dogId = 1;
            var allDogs = new List<Dog>
    {
        new Dog { Id = 1 },
        new Dog { Id = 2 },
        new Dog { Id = 3 }
    };
            var matchedDogIds = new List<int> { 1 }; // Dog with ID 1 is already matched

            dogRepositoryMock.Setup(repo => repo.GetAllDogs()).Returns(allDogs);
            matchRepositoryMock.Setup(repo => repo.GetMatchedDogIds(dogId)).Returns(matchedDogIds);

            // Act
            var result = dogService.GetUnmatchedDogs(dogId);

            // Assert
            // Verify that only dogs with IDs 2 and 3 are returned
            Assert.Equal(2, result.Count);
            Assert.Contains(result, d => d.Id == 2);
            Assert.Contains(result, d => d.Id == 3);
            Assert.DoesNotContain(result, d => d.Id == 1); // Dog with ID 1 should not be in the result
        }


        [Fact]
        public void GetDogById_ShouldReturnDog_WhenDogExists()
        {
            // Arrange
            int dogId = 1;
            var expectedDog = new Dog { Id = dogId };
            dogRepositoryMock.Setup(repo => repo.GetDogById(dogId)).Returns(expectedDog);

            // Act
            var result = dogService.GetDogById(dogId);

            // Assert
            Assert.Equal(expectedDog, result);
        }

        [Fact]
        public void GetDogById_ShouldThrowException_WhenDogDoesNotExist()
        {
            // Arrange
            int dogId = 1;
            dogRepositoryMock.Setup(repo => repo.GetDogById(dogId)).Returns((Dog?)null);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => dogService.GetDogById(dogId));
            Assert.Equal($"No dog found with ID {dogId}", exception.Message);
        }

        [Fact]
        public void GetDogsInMeeting_ShouldReturnDogsInMeeting()
        {
            // Arrange
            int meetingId = 1;
            var dogMeetings = new List<DogMeeting>
            {
                new DogMeeting { DogId = 1 },
                new DogMeeting { DogId = 2 }
            };
            var dogs = new List<Dog>
            {
                new Dog { Id = 1 },
                new Dog { Id = 2 },
                new Dog { Id = 3 }
            };

            dogMeetingRepositoryMock.Setup(repo => repo.GetDogsInMeeting(meetingId)).Returns(dogMeetings);
            dogRepositoryMock.Setup(repo => repo.GetDogById(It.IsAny<int>())).Returns<int>(id => dogs.FirstOrDefault(d => d.Id == id));

            // Act
            var result = dogService.GetDogsInMeeting(meetingId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, d => d.Id == 1);
            Assert.Contains(result, d => d.Id == 2);
        }

        [Fact]
        public void UpdateDog_ShouldCallUpdateDogOnRepository()
        {
            // Arrange
            var dog = new Dog { Id = 1 };

            // Act
            dogService.UpdateDog(dog);

            // Assert
            dogRepositoryMock.Verify(repo => repo.UpdateDog(dog), Times.Once);
        }
    }
}
