using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.BusinessLogic.Services
{
    public class MatchService
    {
        private readonly MatchRepository matchRepository;

        public MatchService(MatchRepository matchRepository)
        {
            this.matchRepository = matchRepository;
        }

        public Match GetMatchById(int matchId)
        {
            try
            {
                Match? match = matchRepository.GetMatchById(matchId);
                return match ?? throw new InvalidOperationException($"No Match found with {matchId}"); ;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Match> GetMatchesWithMessages(int dogId)
        {
            try
            {
                return matchRepository.GetMatchesWithMessages(dogId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Match> GetMatchesWithOutMessages(int dogId)
        {
            try
            {
                return matchRepository.GetMatchesWithOutMessages(dogId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public String DeleteMatchById(int matchId)
        {
            try
            {
                matchRepository.DeleteMatchById(matchId);
                return "ELIMINADO";
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Match? GetMatchByDogs(int dogId1, int dogId2)
        {
            try
            {
                return matchRepository.GetMatchByDogs(dogId1, dogId2);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Match CreateOrUpdateMatch(Match match)
        {
            try
            {
                matchRepository.CreateOrUpdateMatch(match);
                return match;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
