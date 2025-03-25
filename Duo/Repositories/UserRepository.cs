using System.Data;
using Microsoft.Data.SqlClient;
using Duo.Models;
using Duo.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
                new SqlParameter("@Streak", user.Streak)
            };

            DataLink.ExecuteNonQuery("UpdateUser", parameters);
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

     

        internal User GetUserByCredentials(string username, string password)
        {
            var user = GetUserByUsername(username);
            if (user != null && user.Password == password)
            {
                return user;
            }

            return null; // Either user not found or password doesn't match
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            try
            {
                DataTable dataTable = DataLink.ExecuteReader("GetAllUsers", null);

                foreach (DataRow row in dataTable.Rows)
                {
                    users.Add(MapUser(row));
                }

                return users;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }

        }

        public List<User> GetUserFriends(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.");
            }

            List<User> friends = new List<User>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            try
            {
                DataTable dataTable = DataLink.ExecuteReader("GetUserFriends", parameters);

                foreach (DataRow row in dataTable.Rows)
                {  
                    int friendId = Convert.ToInt32(row["UserId1"]) == userId
                        ? Convert.ToInt32(row["UserId2"])
                        : Convert.ToInt32(row["UserId1"]);

                    User friend = GetUserById(friendId);

                    if (friend != null)
                    {
                        friends.Add(friend);
                    }
                }
                return friends;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
        }

        private User GetUserById(int userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            try
            {
                DataTable? dataTable = DataLink.ExecuteReader("GetUserById", parameters);
                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }

                var row = dataTable.Rows[0];
                return MapUser(row); 
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
        }

    }
}
