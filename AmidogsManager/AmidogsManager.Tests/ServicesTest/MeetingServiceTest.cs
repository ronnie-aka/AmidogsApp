using Moq;
using Xunit;
using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Interfaces;
using AmidogsManager.BusinessLogic.Services;
using System.Collections.Generic;
using System.Linq;

namespace AmidogsManager.Tests.ServicesTest
{
    public class MeetingServiceTest
    {
        private readonly Mock<IMeetingRepository> meetingRepositoryMock;
        private readonly Mock<IDogMeetingRepository> dogMeetingRepositoryMock;
        private readonly MeetingService meetingService;

        public MeetingServiceTest()
        {
            meetingRepositoryMock = new Mock<IMeetingRepository>();
            dogMeetingRepositoryMock = new Mock<IDogMeetingRepository>();
            meetingService = new MeetingService(dogMeetingRepositoryMock.Object, meetingRepositoryMock.Object);
        }

        [Fact]
        public void GetMeetingById_ShouldReturnMeeting_WhenMeetingExists()
        {
            // Arrange
            int meetingId = 1;
            var expectedMeeting = new Meeting { Id = meetingId };
            meetingRepositoryMock.Setup(repo => repo.GetMeetingById(meetingId)).Returns(expectedMeeting);

            // Act
            var result = meetingService.GetMeetingById(meetingId);

            // Assert
            Assert.Equal(expectedMeeting, result);
        }

        [Fact]
        public void GetMeetingById_ShouldReturnNull_WhenMeetingDoesNotExist()
        {
            // Arrange
            int meetingId = 1;
            meetingRepositoryMock.Setup(repo => repo.GetMeetingById(meetingId)).Returns((Meeting?)null);

            // Act
            var result = meetingService.GetMeetingById(meetingId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetMeetingsWithDog_ShouldReturnMeetings_WithDog()
        {
            // Arrange
            int dogId = 1;
            var dogMeetings = new List<DogMeeting>
            {
                new DogMeeting { MeetingId = 1 },
                new DogMeeting { MeetingId = 2 }
            };
            var meetings = new List<Meeting>
            {
                new Meeting { Id = 1 },
                new Meeting { Id = 2 }
            };

            dogMeetingRepositoryMock.Setup(repo => repo.GetDogMeetingsWithdog(dogId)).Returns(dogMeetings);
            meetingRepositoryMock.Setup(repo => repo.GetMeetingById(1)).Returns(meetings[0]);
            meetingRepositoryMock.Setup(repo => repo.GetMeetingById(2)).Returns(meetings[1]);

            // Act
            var result = meetingService.GetMeetingsWithDog(dogId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(meetings[0], result);
            Assert.Contains(meetings[1], result);
        }
       
        [Fact]
        public void GetMeetingsByOwnerDog_ShouldReturnMeetings_ByOwnerDog()
        {
            // Arrange
            int dogId = 1;
            var dogMeetings = new List<DogMeeting>
            {
                new DogMeeting { MeetingId = 1 },
                new DogMeeting { MeetingId = 2 }
            };
            var meetings = new List<Meeting>
            {
                new Meeting { Id = 1 },
                new Meeting { Id = 2 }
            };

            dogMeetingRepositoryMock.Setup(repo => repo.GetDogMeetingByOwnerDog(dogId)).Returns(dogMeetings);
            meetingRepositoryMock.Setup(repo => repo.GetMeetingById(1)).Returns(meetings[0]);
            meetingRepositoryMock.Setup(repo => repo.GetMeetingById(2)).Returns(meetings[1]);

            // Act
            var result = meetingService.GetMeetingsByOwnerDog(dogId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(meetings[0], result);
            Assert.Contains(meetings[1], result);
        }

        [Fact]
        public void DeleteMeetingById_ShouldReturnDeletedMessage_WhenMeetingExists()
        {
            // Arrange
            int meetingId = 1;
            meetingRepositoryMock.Setup(repo => repo.DeleteMeetingById(meetingId));
            dogMeetingRepositoryMock.Setup(repo => repo.DeleteDogMeetingById(meetingId));

            // Act
            var result = meetingService.DeleteMeetingById(meetingId);

            // Assert
            Assert.Equal("ELIMINADO", result);
            meetingRepositoryMock.Verify(repo => repo.DeleteMeetingById(meetingId), Times.Once);
            dogMeetingRepositoryMock.Verify(repo => repo.DeleteDogMeetingById(meetingId), Times.Once);
        }

        [Fact]
        public void UpdateMeeting_ShouldReturnUpdatedMessage_WhenMeetingExists()
        {
            // Arrange
            int meetingId = 1;
            var updatedMeeting = new Meeting
            {
                MeetingTitle = "Updated Title",
                MaxParticpants = 10,
                Description = "Updated Description",
                Date = DateTime.Now,
                Location = "Updated Location"
            };
            var existingMeeting = new Meeting { Id = meetingId };

            meetingRepositoryMock.Setup(repo => repo.GetMeetingById(meetingId)).Returns(existingMeeting);
            meetingRepositoryMock.Setup(repo => repo.UpdateMeeting(It.IsAny<Meeting>()));

            // Act
            var result = meetingService.UpdateMeeting(meetingId, updatedMeeting);

            // Assert
            Assert.Equal("UPDATED", result);
            meetingRepositoryMock.Verify(repo => repo.UpdateMeeting(It.Is<Meeting>(m =>
                m.MeetingTitle == updatedMeeting.MeetingTitle &&
                m.MaxParticpants == updatedMeeting.MaxParticpants &&
                m.Description == updatedMeeting.Description &&
                m.Date == updatedMeeting.Date &&
                m.Location == updatedMeeting.Location
            )), Times.Once);
        }

        [Fact]
        public void CreateMeeting_ShouldReturnCreatedMessage_WhenMeetingIsNew()
        {
            // Arrange
            var newMeeting = new Meeting { Id = 1 };
            meetingRepositoryMock.Setup(repo => repo.GetMeetingById(newMeeting.Id)).Returns((Meeting?)null);
            meetingRepositoryMock.Setup(repo => repo.AddMeeting(newMeeting));

            // Act
            var result = meetingService.CreateMeeting(newMeeting);

            // Assert
            Assert.Equal("CREATED", result);
            meetingRepositoryMock.Verify(repo => repo.AddMeeting(newMeeting), Times.Once);
        }

        [Fact]
        public void CreateMeeting_ShouldReturnAlreadyExistsMessage_WhenMeetingAlreadyExists()
        {
            // Arrange
            var existingMeeting = new Meeting { Id = 1 };
            meetingRepositoryMock.Setup(repo => repo.GetMeetingById(existingMeeting.Id)).Returns(existingMeeting);

            // Act
            var result = meetingService.CreateMeeting(existingMeeting);

            // Assert
            Assert.Equal("Meeting already exists with the same ID", result);
            meetingRepositoryMock.Verify(repo => repo.AddMeeting(It.IsAny<Meeting>()), Times.Never);
        }
    }
}
