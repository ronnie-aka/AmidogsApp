using AmidogsManager.Database;
using AmidogsManager.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.Repository.Repositories
{
    public class DogRepository
    {
        private readonly AmidogsManagerContext amidogsManagerContext;
        public DogRepository(AmidogsManagerContext amidogsManagerContext)
        {
            this.amidogsManagerContext = amidogsManagerContext;
        }

        public Dog? GetByUser(int userId) 
        {
            return amidogsManagerContext.Dogs.Where(d => d.UserId == userId).FirstOrDefault();
        }
        public List<Dog> GetAllDogs()
        {
            return amidogsManagerContext.Dogs.ToList();
        }

        public Dog? GetDogById(int dogId)
        {
            return amidogsManagerContext.Dogs.Where(d => d.Id == dogId).FirstOrDefault();
        }

        public void UpdateDog(Dog updatedDog)
        {
            var existingDog = amidogsManagerContext.Dogs.Find(updatedDog.Id);
            if (existingDog != null)
            {
                // Actualizar propiedades del perro existente
                existingDog.DogName = updatedDog.DogName;
                existingDog.Sex = updatedDog.Sex;
                existingDog.Sterilized = updatedDog.Sterilized;
                existingDog.Dominant = updatedDog.Dominant;
                existingDog.Photo = updatedDog.Photo;
                existingDog.Presentation = updatedDog.Presentation;
                existingDog.Breed = updatedDog.Breed;
                existingDog.AgeCategory = updatedDog.AgeCategory;
                existingDog.Personaliity = updatedDog.Personaliity;
                existingDog.Size = updatedDog.Size;

                // Guardar los cambios en la base de datos
                amidogsManagerContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"No dog found with ID {updatedDog.Id}");
            }
        }
    }
}
