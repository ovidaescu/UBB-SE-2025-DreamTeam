using System.Data;
using Microsoft.Data.SqlClient;
using Duo.Models;
using Duo.Data;

namespace Duo.Repositories
{
    public class UserRepository
    {
        public DataLink DataLink { get; }

        public UserRepository(DataLink dataLink)
        {
            DataLink = dataLink;
        }

        public void CreateUser(User user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PrivacyStatus", user.PrivacyStatus),
                new SqlParameter("@OnlineStatus", user.OnlineStatus),
                new SqlParameter("@DateJoined", user.DateJoined),
                new SqlParameter("@ProfileImage", user.ProfileImage ?? ""),
                new SqlParameter("@TotalPoints", user.TotalPoints),
                new SqlParameter("@CoursesCompleted", user.CoursesCompleted),
                new SqlParameter("@QuizzesCompleted", user.QuizzesCompleted),
                new SqlParameter("@Streak", user.Streak)
            };



            DataLink.ExecuteNonQuery("CreateUser", parameters);
        }
    }
}