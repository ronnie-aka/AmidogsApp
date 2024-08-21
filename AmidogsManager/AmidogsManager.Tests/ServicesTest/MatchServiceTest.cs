using Moq;
using Xunit;
using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Interfaces;
using AmidogsManager.BusinessLogic.Services;
using System.Collections.Generic;

namespace AmidogsManager.Tests.ServicesTest
{
    public class MatchServiceTest
    {
        private readonly Mock<IMatchRepository> matchRepositoryMock;
        private readonly MatchService matchService;

        public MatchServiceTest()
        {
            matchRepositoryMock = new Mock<IMatchRepository>();
            matchService = new MatchService(matchRepositoryMock.Object);
        }

        [Fact]
        public void GetMatchById_ShouldReturnMatch_WhenMatchExists()
        {
            int matchId = 1;
            var expectedMatch = new Database.Models.Match { Id = matchId };
            matchRepositoryMock.Setup(repo => repo.GetMatchById(matchId)).Returns(expectedMatch);

            var result = matchService.GetMatchById(matchId);

            Assert.Equal(expectedMatch, result);
        }

        [Fact]
        public void GetMatchById_ShouldThrowException_WhenMatchDoesNotExist()
        {
            int matchId = 1;
            matchRepositoryMock.Setup(repo => repo.GetMatchById(matchId)).Returns((Database.Models.Match?)null);

            var exception = Assert.Throws<InvalidOperationException>(() => matchService.GetMatchById(matchId));
            Assert.Equal($"No Match found with {matchId}", exception.Message);
        }

        [Fact]
        public void GetMatchesWithMessages_ShouldReturnMatchesWithMessages()
        {
            int dogId = 1;
            var matches = new List<Database.Models.Match>
            {
                new Database.Models.Match { DogId1 = dogId, DogId2 = 2, Chat = "Hello", LikeDog1 = true, LikeDog2 = true },
                new Database.Models.Match { DogId1 = dogId, DogId2 = 3, Chat = "Hi", LikeDog1 = true, LikeDog2 = true }
            };
            matchRepositoryMock.Setup(repo => repo.GetMatchesWithMessages(dogId)).Returns(matches);

            var result = matchService.GetMatchesWithMessages(dogId);

            Assert.Equal(2, result.Count);
            Assert.All(result, m => Assert.False(string.IsNullOrWhiteSpace(m.Chat)));
        }

        [Fact]
        public void GetMatchesWithOutMessages_ShouldReturnMatchesWithoutMessages()
        {
            // Arrange
            int dogId = 1;
            var matches = new List<Database.Models.Match>
            {
                new Database.Models.Match { DogId1 = dogId, DogId2 = 2, Chat = "", LikeDog1 = true, LikeDog2 = true },
                new Database.Models.Match { DogId1 = dogId, DogId2 = 3, Chat = null, LikeDog1 = true, LikeDog2 = true }
            };
            matchRepositoryMock.Setup(repo => repo.GetMatchesWithOutMessages(dogId)).Returns(matches);

            // Act
            var result = matchService.GetMatchesWithOutMessages(dogId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, m => Assert.True(string.IsNullOrWhiteSpace(m.Chat)));
        }

        [Fact]
        public void DeleteMatchById_ShouldReturnDeletedMessage_WhenMatchExists()
        {
            // Arrange
            int matchId = 1;
            matchRepositoryMock.Setup(repo => repo.DeleteMatchById(matchId));

            // Act
            var result = matchService.DeleteMatchById(matchId);

            // Assert
            Assert.Equal("ELIMINADO", result);
            matchRepositoryMock.Verify(repo => repo.DeleteMatchById(matchId), Times.Once);
        }

        [Fact]
        public void GetMatchByDogs_ShouldReturnMatch_WhenMatchExists()
        {
            // Arrange
            int dogId1 = 1;
            int dogId2 = 2;
            var expectedMatch = new Database.Models.Match { DogId1 = dogId1, DogId2 = dogId2 };
            matchRepositoryMock.Setup(repo => repo.GetMatchByDogs(dogId1, dogId2)).Returns(expectedMatch);

            // Act
            var result = matchService.GetMatchByDogs(dogId1, dogId2);

            // Assert
            Assert.Equal(expectedMatch, result);
        }

        [Fact]
        public void CreateOrUpdateMatch_ShouldCallCreateOrUpdateMatchOnRepository()
        {
            // Arrange
            var match = new Database.Models.Match { Id = 1, DogId1 = 1, DogId2 = 2, LikeDog1 = true, LikeDog2 = false };

            // Act
            var result = matchService.CreateOrUpdateMatch(match);

            // Assert
            matchRepositoryMock.Verify(repo => repo.CreateOrUpdateMatch(match), Times.Once);
            Assert.Equal(match, result);
        }
    }
}
