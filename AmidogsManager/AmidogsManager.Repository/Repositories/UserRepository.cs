using AmidogsManager.Database;
using AmidogsManager.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmidogsManager.Repository.Repositories
{
    public class UserRepository
    {
        private readonly AmidogsManagerContext amidogsManagerContext;
        public UserRepository(AmidogsManagerContext amidogsManagerContext)
        {
            this.amidogsManagerContext = amidogsManagerContext;
        }
        public User? GetUserById(int userId)
        {
            return amidogsManagerContext.Users.Where(u => u.Id == userId).FirstOrDefault();
        }
        public User? GetUserByDogId(int dogId) {
            return amidogsManagerContext.Users.Where(u => u.DogId == dogId).FirstOrDefault();
        }
        public void UpdateComplaintNumber(int userId)
        {
            // Obtener el usuario por ID
            var user = GetUserById(userId);

            // Verificar si el usuario existe
            if (user != null)
            {
                // Incrementar el número de denuncias
                user.Complaint++;

                // Guardar los cambios en la base de datos
                amidogsManagerContext.SaveChanges();
            }
            else
            {
                // Manejar el caso en que el usuario no se encuentra
                throw new Exception("Usuario no encontrado");
            }
        }
        public User? GetUserByEmail(string email)
        {
            return amidogsManagerContext.Users.Where(u => u.Email == email).FirstOrDefault();
        }

    }
}
