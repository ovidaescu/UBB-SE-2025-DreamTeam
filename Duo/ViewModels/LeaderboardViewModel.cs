﻿using Duo.Models;
using Duo.Services;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Duo.Views.Pages
{
    internal class LeaderBoardViewModel
    {
        private readonly LeaderboardService _leaderboardService;

        public LeaderBoardViewModel()
        {
            _leaderboardService = new LeaderboardService();
        }
        public int Rank { get; set; }
        public string Username { get; set; }
        public decimal Accuracy { get; set; }

        public List<LeaderboardEntry> GetGlobalLeaderboard(string criteria)
        {

            return _leaderboardService.GetGlobalLeaderboard(criteria);
        }

        public List<LeaderboardEntry> GetFriendsLeaderboard(int userId, string criteria)
        {
            return _leaderboardService.GetFriendsLeaderboard(userId, criteria);
        }

        public int GetCurrentUserGlobalRank(int userId, string criteria)
        {
            var users = _leaderboardService.GetGlobalLeaderboard(criteria);
            var currentUser = users.FirstOrDefault(user => user.UserId == userId);
            if (currentUser == null)
            {
                return -1;
            }
            return users.IndexOf(currentUser) + 1;
        }

        public int GetCurrentUserFriendsRank(int userId, string criteria)
        {
            var users = _leaderboardService.GetFriendsLeaderboard(userId, criteria);
            var currentUser = users.FirstOrDefault(user => user.UserId == userId);
            if (currentUser == null)
            {
                return -1;
            }
            return users.IndexOf(currentUser) + 1;
        }

        public List<Course> GetCourses()
        {
            return _leaderboardService.GetCourses();
        }

        public List<LeaderboardEntry> GetTopUsersForCourse(int courseId)
        {
            return _leaderboardService.GetTopUsersForCourse(courseId);
        }

        public List<LeaderboardEntry> GetTopFriendsForCourse(int userId, int courseId)
        {
            return _leaderboardService.GetTopFriendsForCourse(userId, courseId);
        }

        public int GetCurrentUserCourseRank(int userId, int courseId)
        {
            var users = _leaderboardService.GetTopUsersForCourse(courseId);
            var currentUser = users.FirstOrDefault(user => user.UserId == userId);
            if (currentUser == null)
            {
                return -1;
            }
            return users.IndexOf(currentUser) + 1;
        }

        public int GetCurrentUserFriendsCourseRank(int userId, int courseId)
        {
            var users = _leaderboardService.GetTopFriendsForCourse(userId, courseId);
            var currentUser = users.FirstOrDefault(user => user.UserId == userId);
            if (currentUser == null)
            {
                return -1;
            }
            return users.IndexOf(currentUser) + 1;
        }

    }
}