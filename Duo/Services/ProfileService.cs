using Duo.Models;
using Duo.Repositories;
using Duo;
using DuolingoNou.Models;
using System.Collections.Generic;
using System.Linq;

namespace Duo.Services
{
    public class ProfileService
    {
        private readonly UserRepository _userRepository;

        public ProfileService()
        {
            _userRepository = new UserRepository(App.userRepository.DataLink);
        }

        public void CreateUser(User user)
        {
            _userRepository.CreateUser(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        public User GetUserStats(int userId)
        {
            return _userRepository.GetUserStats(userId);
        }



        public void AwardAchievements(User user)
        {
            List<Achievement> achievements = _userRepository.GetAllAchievements();
            List<Achievement> userAchievements = _userRepository.GetUserAchievements(user.UserId);

            // Separate achievements by type and sort them highest to lowest
            var streakAchievements = achievements.Where(a => a.Name.Contains("Streak"))
                                                 .OrderByDescending(a => GetAchievementThreshold(a.Name)).ToList();
            var quizAchievements = achievements.Where(a => a.Name.Contains("Quizzes Completed"))
                                               .OrderByDescending(a => GetAchievementThreshold(a.Name)).ToList();
            var courseAchievements = achievements.Where(a => a.Name.Contains("Courses Completed"))
                                                 .OrderByDescending(a => GetAchievementThreshold(a.Name)).ToList();

            // Award highest achievement per category
            AwardCategoryAchievement(user, streakAchievements, user.Streak, ref userAchievements);
            AwardCategoryAchievement(user, quizAchievements, user.QuizzesCompleted, ref userAchievements);
            AwardCategoryAchievement(user, courseAchievements, user.CoursesCompleted, ref userAchievements);
        }

        private void AwardCategoryAchievement(User user, List<Achievement> categoryAchievements, int statValue, ref List<Achievement> userAchievements)
        {
            foreach (var achievement in categoryAchievements)
            {
                if (statValue >= GetAchievementThreshold(achievement.Name) &&
                    !userAchievements.Any(a => a.Id == achievement.Id))
                {
                    _userRepository.AwardAchievement(user.UserId, achievement.Id);
                    System.Diagnostics.Debug.WriteLine($"Awarded achievement: {achievement.Name}");

                    // **Manually update the local userAchievements list**
                    userAchievements.Add(achievement);

                    break; // Only award the highest achievement in this category
                }
            }
        }



        private int GetAchievementThreshold(string achievementName)
        {
            if (achievementName.Contains("10")) return 10;
            if (achievementName.Contains("50")) return 50;
            if (achievementName.Contains("100")) return 100;
            if (achievementName.Contains("250")) return 250;
            if (achievementName.Contains("500")) return 500;
            if (achievementName.Contains("1000")) return 1000;
            return 0;
        }

        public List<Achievement> GetUserAchievements(int userId)
        {
            return _userRepository.GetUserAchievements(userId);
        }
    }


}
