using Duo.Data;
using Duo.Helpers;
using Duo.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duo.Repositories
{
    class FriendsRepository
    {
        DataLink DataLink { get; }

        public FriendsRepository(DataLink dataLink)
        {
            DataLink = dataLink;
        }

        public void AddFriend(int userId, int friendId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@FriendId", friendId)
            };
            DataLink.ExecuteNonQuery("AddFriend", parameters);
        }

        public List<User> GetTopFriendsByCompletedQuizzes()
        {
            var DataTable = DataLink.ExecuteReader("GetTopUsersByCompletedQuizzes");
            List<User> users = new List<User>();
            foreach (DataRow row in DataTable.Rows)
            {
                users.Add(Mappers.MapUser(row));
            }

            return users;
        }
        public List<User> GetTopFriendsByAccuracy()
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
