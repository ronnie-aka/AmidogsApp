using AmidogsManager.Database.Models;

namespace AmidogsManager.Repository.Interfaces
{
    public interface IMeetingRepository
    {
        Meeting? GetMeetingById(int meetingId);
        void DeleteMeetingById(int meetingId);
        void UpdateMeeting(Meeting meeting);
        void AddMeeting(Meeting meeting);
    }
}
