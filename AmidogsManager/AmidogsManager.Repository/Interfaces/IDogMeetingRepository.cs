using AmidogsManager.Database.Models;
using System.Collections.Generic;

namespace AmidogsManager.Repository.Interfaces
{
    public interface IDogMeetingRepository
    {
        List<DogMeeting> GetDogsInMeeting(int meetingId);
        List<DogMeeting> GetDogMeetingsWithdog(int dogId);
        List<DogMeeting> GetDogMeetingsWithOut(int dogId);
        List<DogMeeting> GetDogMeetingByOwnerDog(int dogId);
        void DeleteDogMeetingById(int meetingId);
        void AddDogToMeeting(int dogId, int meetingId, bool isOwner);
        void RemoveDogFromMeeting(int dogId, int meetingId);
    }
}
