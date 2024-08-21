using AmidogsManager.Database.Models;
using System.Collections.Generic;

namespace AmidogsManager.Repository.Interfaces
{
    public interface IDogRepository
    {
        Dog? GetByUser(int userId);
        List<Dog> GetAllDogs();
        Dog? GetDogById(int dogId);
        void UpdateDog(Dog updatedDog);
    }
}
