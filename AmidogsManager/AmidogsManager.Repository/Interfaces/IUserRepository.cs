using AmidogsManager.Database.Models;

namespace AmidogsManager.Repository.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserById(int userId);
        User? GetUserByDogId(int dogId);
        void UpdateComplaintNumber(int userId);
        User? GetUserByEmail(string email);
    }
}
