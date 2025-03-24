using System.Data;
using Microsoft.Data.SqlClient;
using Duo.Models;
using Duo.Data;
using System.Collections.Generic;
using System;
using Duo.Helpers;

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
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@PrivacyStatus", user.PrivacyStatus),
                new SqlParameter("@OnlineStatus", user.OnlineStatus),
                new SqlParameter("@DateJoined", user.DateJoined),
                new SqlParameter("@ProfileImage", user.ProfileImage ?? ""),
                new SqlParameter("@TotalPoints", user.TotalPoints),
                new SqlParameter("@CoursesCompleted", user.CoursesCompleted),
                new SqlParameter("@QuizzesCompleted", user.QuizzesCompleted),
                new SqlParameter("@Streak", user.Streak)
                new SqlParameter("@Accuracy", user.Accuracy)

            };
            DataLink.ExecuteNonQuery("CreateUser", parameters);
        }

        public List<User> GetTopUsersByCompletedQuizzes()
        {
            var DataTable = DataLink.ExecuteReader("GetTopUsersByCompletedQuizzes");
            List<User> users = new List<User>();
            foreach (DataRow row in DataTable.Rows)
            {
                users.Add(Mappers.MapUser(row));
            }

            return users;
        }
        public List<User> GetTopUsersByAccuracy()
        {
            var DataTable = DataLink.ExecuteReader("GetTopUsersByAccuracy");
            List<User> users = new List<User>();
            foreach (DataRow row in DataTable.Rows)
            {
                users.Add(Mappers.MapUser(row));
            }

            return users;
        }


    }
}