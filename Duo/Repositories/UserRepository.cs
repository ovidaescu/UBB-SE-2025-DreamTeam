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
                return Mappers.MapUser(dataTable.Rows[0]);
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
                return Mappers.MapUser(dataTable.Rows[0]);
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
                new SqlParameter("@Streak", user.Streak),
                new SqlParameter("@Accuracy", user.Accuracy)

            };
            DataLink.ExecuteNonQuery("CreateUser", parameters);
        }


        public void UpdateUser(User user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", user.UserId),
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
                new SqlParameter("@Streak", user.Streak),
                new SqlParameter("@Accuracy", user.Accuracy)
            };

            DataLink.ExecuteNonQuery("UpdateUser", parameters);
        }


        public bool ValidateCredentials(string username, string password)
        {
            User? user = GetUserByUsername(username);
            return user != null && user.Password == password;
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

        internal User GetUserByCredentials(string username, string password)
        {
            var user = GetUserByUsername(username);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null; // Either user not found or password doesn't match
        }



    }
}