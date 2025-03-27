using System.Data;
using Microsoft.Data.SqlClient;
using Duo.Models;
using Duo.Data;
using System.Collections.Generic;
using System;
using Duo.Helpers;
using DuolingoNou.Models;

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

        public int CreateUser(User user)
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
                new SqlParameter("@LastActivityDate", user.LastActivityDate ?? (object)DBNull.Value),
                new SqlParameter("@Accuracy", user.Accuracy)
            };

            // Use ExecuteScalar to return the newly inserted UserId
            object result = DataLink.ExecuteScalar<int>("CreateUser", parameters);

            // Convert result to int (handle null safety)
            return result != null ? Convert.ToInt32(result) : -1;
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
                new SqlParameter("@LastActivityDate", user.LastActivityDate ?? (object)DBNull.Value),
                new SqlParameter("@Accuracy", user.Accuracy)
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
                Password = row["Password"].ToString()!,
                Accuracy = Convert.ToDecimal(row["Accuracy"])

            };
        }

        public List<LeaderboardEntry> GetTopUsersByCompletedQuizzes()
        {
            var DataTable = DataLink.ExecuteReader("GetTopUsersByCompletedQuizzes");
            List<LeaderboardEntry> users = new List<LeaderboardEntry>();
            int index = 1;
            foreach (DataRow row in DataTable.Rows)
            {

                users.Add( new LeaderboardEntry()
                {
                    Rank = index++,
                    UserId = Convert.ToInt32(row["UserId"]),
                    Username = row["UserName"].ToString()!,
                    CompletedQuizzes = Convert.ToInt32(row["QuizzesCompleted"]),
                    Accuracy = Convert.ToDecimal(row["Accuracy"]),
                    ProfilePicture = ".. / .. / Assets /" + row["ProfileImage"].ToString()!
                });
            }

            return users;
        }

        public List<LeaderboardEntry> GetTopUsersByAccuracy()
        {
            var DataTable = DataLink.ExecuteReader("GetTopUsersByAccuracy");
            List<LeaderboardEntry> users = new List<LeaderboardEntry>();
            int index = 1;
            foreach (DataRow row in DataTable.Rows)
            {
                users.Add(new LeaderboardEntry()
                {
                    Rank = index++,
                    UserId = Convert.ToInt32(row["UserId"]),
                    Username = row["UserName"].ToString()!,
                    CompletedQuizzes = Convert.ToInt32(row["QuizzesCompleted"]),
                    Accuracy = Convert.ToDecimal(row["Accuracy"]),
                    ProfilePicture = ".. / .. / Assets /" + row["ProfileImage"].ToString()!
                });
            }
            return users;
        }

        public List<LeaderboardEntry> GetTopUsersForCourse(int courseId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@CourseId", courseId)
            };
            var DataTable = DataLink.ExecuteReader("GetTopUsersForCourse", sqlParameters);
            List<LeaderboardEntry> users = new List<LeaderboardEntry>();
            int index = 1;
            foreach (DataRow row in DataTable.Rows)
            {
                users.Add(new LeaderboardEntry()
                {
                    Rank = index++,
                    UserId = Convert.ToInt32(row["UserId"]),
                    Username = row["Username"].ToString()!,
                    Accuracy = decimal.Parse(Convert.ToDecimal(row["CompletionPercentage"]).ToString("0.00")),
                    CompletedQuizzes = Convert.ToInt32(row["LessonsCompleted"]),
                    ProfilePicture = "../../Assets/" + row["ProfileImage"].ToString()!
                });
            }
            return users;
        }

        public List<Course> GetCourses()
        {
            var DataTable = DataLink.ExecuteReader("GetCourses");
            List<Course> courses = new List<Course>();
            foreach (DataRow row in DataTable.Rows)
            {
                courses.Add(new Course()
                {
                    Id = Convert.ToInt32(row["CourseId"]),
                    Name = row["Name"].ToString()!,
                    TotalNumOfLessons = Convert.ToInt32(row["TotalNumberOfLessons"])
                });
            }
            return courses;
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


        public User GetUserStats(int userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            DataTable dataTable = DataLink.ExecuteReader("GetUserStats", parameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new User
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    UserName = row["UserName"].ToString()!,
                    ProfileImage = row["ProfileImage"].ToString()!,
                    TotalPoints = Convert.ToInt32(row["TotalPoints"]),
                    Streak = Convert.ToInt32(row["Streak"]),
                    QuizzesCompleted = Convert.ToInt32(row["QuizzesCompleted"]),
                    CoursesCompleted = Convert.ToInt32(row["CoursesCompleted"])
                };
            }

            return null;
        }

        public List<Achievement> GetAllAchievements()
        {
            DataTable dataTable = DataLink.ExecuteReader("GetAllAchievements");

            List<Achievement> achievements = new List<Achievement>();
            foreach (DataRow row in dataTable.Rows)
            {
                achievements.Add(new Achievement
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString()!,
                    Description = row["Description"].ToString()!,
                    Rarity = row["Rarity"].ToString()!
                });
            }

            return achievements;
        }

        public List<Achievement> GetUserAchievements(int userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@UserId", userId)
            };

            DataTable dataTable = DataLink.ExecuteReader("GetUserAchievements", parameters);

            List<Achievement> achievements = new List<Achievement>();
            foreach (DataRow row in dataTable.Rows)
            {
                achievements.Add(new Achievement
                {
                    Id = Convert.ToInt32(row["AchievementId"]),
                    Name = row["Name"].ToString()!,
                    Description = row["Description"].ToString()!,
                    Rarity = row["Rarity"].ToString()!,
                    AwardedDate = Convert.ToDateTime(row["AwardedDate"])
                });
            }

            return achievements;
        }

        public void AwardAchievement(int userId, int achievementId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@UserId", userId),
            new SqlParameter("@AchievementId", achievementId),
            new SqlParameter("@AwardedDate", DateTime.Now)
            };

            DataLink.ExecuteNonQuery("AwardAchievement", parameters);
        }

        public List<User> GetFriends(int userId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@UserId", userId)
            };

            DataTable dataTable = DataLink.ExecuteReader("GetFriends", parameters);
            List<User> friends = new List<User>();

            foreach (DataRow row in dataTable.Rows)
            {
                friends.Add(Mappers.MapUser(row));
            }

            return friends;
        }



    }
}