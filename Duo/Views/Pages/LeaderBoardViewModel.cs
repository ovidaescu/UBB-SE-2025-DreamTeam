using Duo.Models;
using System.Collections.Generic;

namespace Duo.Views.Pages
{
    internal class LeaderBoardViewModel
    {
        //LeaderboardService _leaderboardService = new LeaderboardService();

        public List<User> GetGlobalLeaderboard(string criteria)
        {
            //return _leaderboardService.GetGlobalLeaderboard(criteria);
            return new List<User>();
        }

        public int GetUserRank(int userId)
        {
            //return _leaderboardService.GetUserRank(userId);
            return 0;
        }

        public void UpdateUserScore(int userId, int points)
        {
            // _leaderboardService.UpdateUserScore(userId, points);
        }

        public void CalculateRankChange(int userId, string timeFrame)
        {
            // _leaderboardService.CalculateRankChange(userId, timeFrame);
        }

        public List<User> GetFriendsLeaderboard(int userId)
        {
            //return _leaderboardService.GetFriendsLeaderboard(userId);
            return new List<User>();
        }

    }
}