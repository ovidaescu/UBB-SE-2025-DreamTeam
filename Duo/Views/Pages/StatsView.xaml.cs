using Duo;
using Duo.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using DuolingoNou.Models;



namespace DuolingoNou.Views.Pages
{
    public sealed partial class StatsView : Page
    {
        public StatsView()
        {
            this.InitializeComponent();  // Connects XAML to the C# code-behind
            LoadStats();  // Call a method to load stats when the page is initialized
        }

        private void LoadStats()
        {
            // Set values for the TextBlocks based on your data logic
            QuizStreakText.Text = "5";  // Example of setting the Quiz Streak to 5
            CourseStreakText.Text = "10";  // Example of setting the Course Streak to 10
            CompletedQuizText.Text = "20";  // Set the number of completed quizzes
            CompletedCourseText.Text = "3";  // Set the completed streak value
            TotalQuizXPText.Text = "500";  // Total XP from quizzes
            TotalCourseXPText.Text = "800";  // Total XP from courses

            // Create a list of achievements for quizzes and courses
            List<Achievement> quizAchievements = new List<Achievement>
            {
                new Achievement("Completed 5 Courses", 100, "Completed"),
                new Achievement("Completed a Course in 1 Week", 85, "Completed"),
                new Achievement("Completed 10 Courses", 90, "Completed"),
                new Achievement("Achieved 50% Course Completion Rate", 80, "In Progress"),
                new Achievement("Completed 3 Advanced Courses", 95, "Completed"),
                new Achievement("Course Master: Finished 20 Courses", 100, "Completed"),
                new Achievement("Streak: Completed a Course Every Month for 6 Months", 80, "In Progress")

            };

            List<Achievement> courseAchievements = new List<Achievement>
            {
                new Achievement("Completed 500 Quizzes", 100, "Completed"),
                new Achievement("Completed 100 Quizzes in 30 Days", 85, "Completed"),
                new Achievement("Completed 50 Quizzes in a Week", 90, "Completed"),
                new Achievement("Achieved 60-Day Streak", 100, "Completed"),
                new Achievement("Completed 10 Quizzes in a Day", 70, "Completed"),
                new Achievement("Passed 75% of Quizzes", 75, "In Progress"),
                new Achievement("Achieved 30-Day Streak", 80, "In Progress")

            };

            // Bind the ListBoxes to the list of achievements
            QuizListBox.ItemsSource = quizAchievements;
            CourseListBox.ItemsSource = courseAchievements;
        }
        // Handle the Tapped event for an achievement
        private async void OnAchievementTapped(object sender, RoutedEventArgs e)
        {
            // Identify which ListBoxItem was tapped
            var tappedItem = sender as ListBoxItem;
            if (tappedItem != null)
            {
                // Optionally, you can use the `DataContext` to get information about the tapped achievement
                var achievement = tappedItem.DataContext as Achievement;
                if (achievement != null)
                {
                    // Create and show a ContentDialog
                    var contentDialog = new ContentDialog
                    {
                        Title = "Achievement Details",
                        Content = $"{achievement.Title} - {achievement.CompletionStatus}",
                        CloseButtonText = "OK"
                    };

                    // Show the dialog asynchronously
                    await contentDialog.ShowAsync();
                }
            }
        }

    }
}

