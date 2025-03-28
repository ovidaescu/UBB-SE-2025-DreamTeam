﻿using Duo.Models;
using Duo.Repositories;
using System.Collections.Generic;

namespace Duo.Services;

class LeaderboardService
{
    private readonly UserRepository _userRepository;
    private readonly FriendsRepository _friendsRepository;

    public LeaderboardService()
    {
        _userRepository = new UserRepository(App.userRepository.DataLink);
        _friendsRepository = new FriendsRepository(App.friendsRepository.DataLink);
    }

    public List<LeaderboardEntry> GetGlobalLeaderboard(string criteria)
    {
        //return the first 10 users in the repo sorted by completed quizzes
        if (criteria == "CompletedQuizzes")
        {
            return _userRepository.GetTopUsersByCompletedQuizzes();
        }
        //return the first 10 users in the repo sorted by accurracy
        else if (criteria == "Accuracy")
        {
            return _userRepository.GetTopUsersByAccuracy();
        }
        else
        {
            throw new System.Exception("Invalid criteria");
        }
    }
    public List<LeaderboardEntry> GetFriendsLeaderboard(int userId, string criteria)
    {
        //return the first 10 users in the repo sorted by completed quizzes
        if (criteria == "CompletedQuizzes")
        {
            return _friendsRepository.GetTopFriendsByCompletedQuizzes(userId);
        }
        //return the first 10 users in the repo sorted by accurracy
        else if (criteria == "Accuracy")
        {
            return _friendsRepository.GetTopFriendsByAccuracy(userId);
        }
        else
        {
            throw new System.Exception("Invalid criteria");
        }
    }

    public List<LeaderboardEntry> GetTopUsersForCourse(int courseId)
    {
        return _userRepository.GetTopUsersForCourse(courseId);
    }

    public List<LeaderboardEntry> GetTopFriendsForCourse(int userId, int courseId)
    {
        return _friendsRepository.GetTopFriendsForCourse(userId, courseId);
    }

    public List<Course> GetCourses()
    {
        return _userRepository.GetCourses();
    }
}

