using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Duo.Models;
using System.Collections.ObjectModel;
using Duo.ViewModels;
using Duo.Helpers;
using System.Data;
using DuolingoNou.Views.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Duo.Views.Pages;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class LeaderboardPage : Page
{
    public ObservableCollection<LeaderboardEntry> Leaderboard { get; set; }
    private LeaderBoardViewModel _leaderBoardViewModel;
    private string _selectedMode = "Global";
    private int currentUserId = 1;
    public LeaderboardPage()
    {
        this.InitializeComponent();
        _leaderBoardViewModel = new LeaderBoardViewModel();
        Leaderboard = new ObservableCollection<LeaderboardEntry>(_leaderBoardViewModel.GetGlobalLeaderboard("Accuracy"));
        LeaderboardListView.ItemsSource = Leaderboard;
        CurrentUserRank.Text = $"Your Rank: {_leaderBoardViewModel.GetCurrentUserGlobalRank(currentUserId, "Accuracy")}";

        List<string> courses = _leaderBoardViewModel.GetCourses();
        AddCoursesToComboBox(courses);
    }

    private void AddCoursesToComboBox(List<string> courses)
    {
        foreach (var course in courses)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = course;
            CoursesComboBox.Items.Add(item);
        }
    }

    // Event handler for Global button click
    private void GlobalButton_Click(object sender, RoutedEventArgs e)
    {
        // Update the Leaderboard to display global rankings
        _selectedMode = "Global";
        LeaderboardListView.ItemsSource = _leaderBoardViewModel.GetGlobalLeaderboard("Accuracy");
        CurrentUserRank.Text = $"Your Rank: {_leaderBoardViewModel.GetCurrentUserGlobalRank(currentUserId, "Accuracy")}";
        RankingCriteriaComboBox.SelectedItem = SortBy;


    }

    // Event handler for Friends button click
    private void FriendsButton_Click(object sender, RoutedEventArgs e)
    {
        // Update the Leaderboard to display friends' rankings
        _selectedMode = "Friends";
        LeaderboardListView.ItemsSource = _leaderBoardViewModel.GetFriendsLeaderboard(currentUserId, "Accuracy");
        CurrentUserRank.Text = $"Your Rank: {_leaderBoardViewModel.GetCurrentUserFriendsRank(currentUserId, "Accuracy")}";
        RankingCriteriaComboBox.SelectedItem = SortBy;

    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        // Refresh the Leaderboard
        if (_selectedMode == "Global")
        {
            Leaderboard = new ObservableCollection<LeaderboardEntry>(_leaderBoardViewModel.GetGlobalLeaderboard("Accuracy"));
            CurrentUserRank.Text = $"Your Rank: {_leaderBoardViewModel.GetCurrentUserGlobalRank(currentUserId, "Accuracy")}";
        }
        else
        {
            Leaderboard = new ObservableCollection<LeaderboardEntry>(_leaderBoardViewModel.GetFriendsLeaderboard(currentUserId, "Accuracy"));
            CurrentUserRank.Text = $"Your Rank: {_leaderBoardViewModel.GetCurrentUserFriendsRank(currentUserId, "Accuracy")}";
        }
        LeaderboardListView.ItemsSource = Leaderboard;

        RankingCriteriaComboBox.SelectedItem = SortBy;
    }

    private void RankingCriteriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Get the selected item
        var selectedItem = (ComboBoxItem)RankingCriteriaComboBox.SelectedItem;

        if (selectedItem != null)
        {
            string selectedCriteria = selectedItem.Content.ToString();

            // Now, you can filter or change the ranking based on the selected criteria
            // For example:
            switch (selectedCriteria)
            {
                case "Accuracy":
                    if (_selectedMode == "Global")
                    {
                        Leaderboard = new ObservableCollection<LeaderboardEntry>(_leaderBoardViewModel.GetGlobalLeaderboard("Accuracy"));
                        CurrentUserRank.Text = $"Your Rank: {_leaderBoardViewModel.GetCurrentUserGlobalRank(currentUserId, "Accuracy")}";
                    }
                    else
                    {
                        Leaderboard = new ObservableCollection<LeaderboardEntry>(_leaderBoardViewModel.GetFriendsLeaderboard(currentUserId, "Accuracy"));
                        CurrentUserRank.Text = $"Your Rank: {_leaderBoardViewModel.GetCurrentUserFriendsRank(currentUserId, "Accuracy")}";
                    }
                    LeaderboardListView.ItemsSource = Leaderboard;
                    break;
                case "Completed Quizzes":
                    if (_selectedMode == "Global")
                    {
                        Leaderboard = new ObservableCollection<LeaderboardEntry>(_leaderBoardViewModel.GetGlobalLeaderboard("CompletedQuizzes"));
                        CurrentUserRank.Text = $"Your Rank: {_leaderBoardViewModel.GetCurrentUserGlobalRank(currentUserId, "CompletedQuizzes")}";
                    }
                    else
                    {
                        Leaderboard = new ObservableCollection<LeaderboardEntry>(_leaderBoardViewModel.GetFriendsLeaderboard(currentUserId, "CompletedQuizzes"));
                        CurrentUserRank.Text = $"Your Rank: {_leaderBoardViewModel.GetCurrentUserFriendsRank(currentUserId, "CompletedQuizzes")}";
                    }
                    LeaderboardListView.ItemsSource = Leaderboard;
                    break;
            }
        }
    }


}
