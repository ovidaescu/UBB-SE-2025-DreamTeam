using Duo.Models;
using Duo.Repositories;
using System.Collections.Generic;

namespace Duo.Services;

class LeaderboardService
{
    private readonly UserRepository _userRepository;
    private readonly FriendsRepository _friendsRepository;

    public LeaderboardService(UserRepository userRepository, FriendsRepository friendsRepository)
    {
        _userRepository = userRepository;
        _friendsRepository = friendsRepository;
    }

    public List<User> GetGlobalLeaderboard(string criteria)
    {
        //return the first 10 users in the repo sorted by completed quizzes
        if (criteria == "QuizzesCompleted")
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
    public List<User> GetFriendsLeaderboard(string criteria)
    {
        //return the first 10 users in the repo sorted by completed quizzes
        if (criteria == "QuizzesCompleted")
        {
            return _friendsRepository.GetTopFriendsByCompletedQuizzes();
        }
        //return the first 10 users in the repo sorted by accurracy
        else if (criteria == "Accuracy")
        {
            return _friendsRepository.GetTopFriendsByAccuracy();
        }
        else
        {
            throw new System.Exception("Invalid criteria");
        }
    }


    public void UpdateUserScore(int userId, int points)
    {
        //TODO: Implement this method
    }
    public void CalculateRankChange(int userId, string timeFrame)
    {
        //TODO: Implement this method
    }

}

