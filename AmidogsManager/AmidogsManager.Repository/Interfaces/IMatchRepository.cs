using AmidogsManager.Database.Models;
using System.Collections.Generic;

namespace AmidogsManager.Repository.Interfaces
{
    public interface IMatchRepository
    {
        Match? GetMatchById(int matchId);
        List<Match> GetMatchesWithMessages(int dogId);
        List<Match> GetMatchesWithOutMessages(int dogId);
        void DeleteMatchById(int matchId);
        List<int> GetMatchedDogIds(int dogId);
        Match? GetMatchByDogs(int dogId1, int dogId2);
        void CreateOrUpdateMatch(Match match);
    }
}
