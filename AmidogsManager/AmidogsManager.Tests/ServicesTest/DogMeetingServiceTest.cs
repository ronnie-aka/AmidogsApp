using Moq;
using Xunit;
using AmidogsManager.BusinessLogic.Services;
using AmidogsManager.Repository.Interfaces;

namespace AmidogsManager.Tests
{
    public class DogMeetingServiceTest
    {
        private readonly Mock<IDogMeetingRepository> mockRepository;
        private readonly DogMeetingService dogMeetingService;

        public DogMeetingServiceTest()
        {
            mockRepository = new Mock<IDogMeetingRepository>();
            dogMeetingService = new DogMeetingService(mockRepository.Object);
        }

        [Fact]
        public void AddDogToMeeting_Call_AddDogToMeeting_OnRepository()
        {
            int dogId = 1;
            int meetingId = 2;
            bool isOwner = true;

            dogMeetingService.AddDogToMeeting(dogId, meetingId, isOwner);

            mockRepository.Verify(r => r.AddDogToMeeting(dogId, meetingId, isOwner), Times.Once);
        }

        [Fact]
        public void RemoveDogFromMeeting_Call_RemoveDogFromMeeting_OnRepository()
        {
            // Arrange
            int dogId = 1;
            int meetingId = 2;

            // Act
            dogMeetingService.RemoveDogFromMeeting(dogId, meetingId);

            // Assert
            mockRepository.Verify(r => r.RemoveDogFromMeeting(dogId, meetingId), Times.Once);
        }
    }
}
