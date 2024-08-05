using AmidogsManager.Database;
using AmidogsManager.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Match = AmidogsManager.Database.Models.Match;

namespace AmidogsManager.Repository.Repositories
{
    public class MatchRepository
    {
        private readonly AmidogsManagerContext amidogsManagerContext;

        public MatchRepository(AmidogsManagerContext amidogsManagerContext)
        {
            this.amidogsManagerContext = amidogsManagerContext;
        }

        public Match? GetMatchById(int matchId) {
            return amidogsManagerContext.Matchs.Where(m => m.Id == matchId).FirstOrDefault();
        }
        public List<Match> GetMatchesWithMessages(int dogId)
        {
            return amidogsManagerContext.Matchs
                .Where(m => (m.DogId1 == dogId || m.DogId2 == dogId) && !string.IsNullOrWhiteSpace(m.Chat) && m.LikeDog1 && m.LikeDog2)
                .ToList();
        }

        public List<Match> GetMatchesWithOutMessages(int dogId)
        {
            return amidogsManagerContext.Matchs
                .Where(m => (m.DogId1 == dogId || m.DogId2 == dogId) && string.IsNullOrWhiteSpace(m.Chat) && m.LikeDog1 && m.LikeDog2)
                .ToList();
        }

        public void DeleteMatchById(int matchId)
        {
            Match? match = amidogsManagerContext.Matchs.Where(m => m.Id == matchId).FirstOrDefault();

            if (match != null)
            {
                amidogsManagerContext.Matchs.Remove(match);
                amidogsManagerContext.SaveChanges();
            }
        }
        public List<int> GetMatchedDogIds(int dogId)
        {
            return amidogsManagerContext.Matchs
                .Where(m => (m.DogId1 == dogId || m.DogId2 == dogId) && m.LikeDog1 && m.LikeDog2)
                .Select(m => m.DogId1 == dogId ? m.DogId2 : m.DogId1)
                .ToList();
        }
        public Match? GetMatchByDogs(int dogId1, int dogId2)
        {
            return amidogsManagerContext.Matchs.FirstOrDefault(m =>
                (m.DogId1 == dogId1 && m.DogId2 == dogId2) ||
                (m.DogId1 == dogId2 && m.DogId2 == dogId1));
        }
        public void CreateOrUpdateMatch(Match match)
        {
            var existingMatch = GetMatchByDogs(match.DogId1, match.DogId2);
            if (existingMatch != null)
            {
                // Solo actualizar el campo correspondiente
                if (existingMatch.DogId1 == match.DogId1)
                {
                    existingMatch.LikeDog1 = match.LikeDog1;
                }
                if (existingMatch.DogId2 == match.DogId2)
                {
                    existingMatch.LikeDog2 = match.LikeDog2;
                }

                amidogsManagerContext.Matchs.Update(existingMatch);
            }
            else
            {
                amidogsManagerContext.Matchs.Add(match);
            }
            amidogsManagerContext.SaveChanges();
        }

    }
}
    