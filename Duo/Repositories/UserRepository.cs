using System.Data;
using Microsoft.Data.SqlClient;
using Duo.Models;
using Duo.Data;
using System;

namespace Duo.Repositories
{
    public class UserRepository
    {
        public DataLink DataLink { get; }

        public UserRepository(DataLink dataLink)
        {
            DataLink = dataLink;
        }

        public User GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Invalid username.");
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };
            DataTable? dataTable = null;
            try
            {
                dataTable = DataLink.ExecuteReader("GetUserByUsername", parameters);
                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }
                var row = dataTable.Rows[0];
                return MapUser(dataTable.Rows[0]);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
            finally
            {
                dataTable?.Dispose();
            }
        }

        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Invalid email.");
            }
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };
            DataTable? dataTable = null;
            try
            {
                dataTable = DataLink.ExecuteReader("GetUserByEmail", parameters);
                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }
                var row = dataTable.Rows[0];
                return MapUser(dataTable.Rows[0]);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
            finally
            {
                dataTable?.Dispose();
            }
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
                
            };
            DataLink.ExecuteNonQuery("CreateUser", parameters);
        }

        public bool ValidateCredentials(string username, string password)
        {
            User? user = GetUserByUsername(username);
            return user != null && user.Password == password;
        }

        private User MapUser(DataRow row)
        {
            return new User
            {
                UserId = Convert.ToInt32(row["UserId"]),
                UserName = row["UserName"].ToString()!,
                Email = row["Email"].ToString()!,
                PrivacyStatus = Convert.ToBoolean(row["PrivacyStatus"]),
                OnlineStatus = Convert.ToBoolean(row["OnlineStatus"]),
                DateJoined = Convert.ToDateTime(row["DateJoined"]),
                ProfileImage = row["ProfileImage"].ToString()!,
                TotalPoints = Convert.ToInt32(row["TotalPoints"]),
                CoursesCompleted = Convert.ToInt32(row["CoursesCompleted"]),
                QuizzesCompleted = Convert.ToInt32(row["QuizzesCompleted"]),
                Streak = Convert.ToInt32(row["Streak"]),
                Password = row["Password"].ToString()!
            };
        }
    }
}