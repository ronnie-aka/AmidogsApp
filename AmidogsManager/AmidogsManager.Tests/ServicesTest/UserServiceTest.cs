using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Interfaces;
using AmidogsManager.BusinessLogic.Services;

namespace AmidogsManager.Tests.ServicesTest
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly UserService userService;

        public UserServiceTest()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            userService = new UserService(userRepositoryMock.Object);
        }

        [Fact]
        public void GetUserById_ShouldReturnUser_WhenUserExists()
        {
            int userId = 1;
            var expectedUser = new User { Id = userId };
            userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(expectedUser);

            var result = userService.GetUserById(userId);

            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public void GetUserById_ShouldThrowException_WhenUserDoesNotExist()
        {
            int userId = 1;
            userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns<User>(null);

  
            var exception = Assert.Throws<InvalidOperationException>(() => userService.GetUserById(userId));
            Assert.Equal($"No Match found with {userId}", exception.Message);
        }

        [Fact]
        public void GetUserByDogId_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            int dogId = 1;
            var expectedUser = new User { DogId = dogId };
            userRepositoryMock.Setup(repo => repo.GetUserByDogId(dogId)).Returns(expectedUser);

            // Act
            var result = userService.GetUserByDogId(dogId);

            // Assert
            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public void GetUserByDogId_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int dogId = 1;
            userRepositoryMock.Setup(repo => repo.GetUserByDogId(dogId)).Returns<User>(null);

            // Act
            var result = userService.GetUserByDogId(dogId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdateComplaintNumber_ShouldReturnSuccess_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(new User { Id = userId, Complaint = 0 });
            userRepositoryMock.Setup(repo => repo.UpdateComplaintNumber(userId));

            // Act
            var result = userService.UpdateComplaintNumber(userId);

            // Assert
            Assert.Equal("Denuncia hecha", result);
            userRepositoryMock.Verify(repo => repo.UpdateComplaintNumber(userId), Times.Once);
        }

        [Fact]
        public void GetUserByEmail_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            string email = "test@example.com";
            var expectedUser = new User { Email = email };
            userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns(expectedUser);

            // Act
            var result = userService.GetUserByEmail(email);

            // Assert
            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public void GetUserByEmail_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            string email = "test@example.com";
            userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns<User>(null);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => userService.GetUserByEmail(email));
            Assert.Equal($"No Match found with email {email}", exception.Message);
        }
    }
}
