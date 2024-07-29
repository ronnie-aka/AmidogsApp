using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.BusinessLogic.Services
{
    public class DogService
    {
        private readonly DogRepository dogRepository;
        private readonly DogMeetingRepository dogMeetingRepository;

        public DogService(DogRepository dogRepository)
        {
            this.dogRepository = dogRepository;
        }

        public Dog getDogByUser(int userId)
        {
            try 
            {
                Dog dog = dogRepository.GetByUser(userId);
                return dog;
            }     
            catch (Exception e){
                throw e; 
            }
        }
    }
}
