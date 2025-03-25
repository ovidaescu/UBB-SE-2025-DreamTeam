using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using DuolingoNou.Views.Pages;
using Duo.Repositories;
using System.Collections.Generic;
using Duo.Models;
using Duo.Data;
using System.Linq;
using Duo;
using Windows.Networking.NetworkOperators;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using Windows.Storage.Streams;
using Duo.ViewModels;
using System.IO;

namespace DuolingoNou.Views
{
    public sealed partial class ShellPage : Page
    {
        private readonly UserRepository _userRepository;
        private List<User> _users;

        public ShellPage()
        {
            this.InitializeComponent();
            _userRepository = App.userRepository; 
            LoadUsers();
            
            ContentFrame.Navigate(typeof(ProfileSettingsPage)); // Default page
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem)
            {
                switch (selectedItem.Tag)
                {
                    case "Settings":
                        ContentFrame.Navigate(typeof(ProfileSettingsPage));
                        break;
                    case "HomePage":
                        ContentFrame.Navigate(typeof(MainPage)); // create this later
                        break;
                        /*
                    case "Quiz":
                        ContentFrame.Navigate(typeof(QuizPage)); // create this later
                        break;*/
                }
            }
        }

        private void LoadUsers()
        {
            _users = _userRepository.GetAllUsers();
            
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text?.Trim().ToLower();

            if (!string.IsNullOrEmpty(query))
            {
                var results = _users.Where(user => user.UserName.ToLower().Contains(query) ||
                                                   user.Email.ToLower().Contains(query)).ToList();

                SearchResultsList.ItemsSource = results;
                SearchResultsList.Visibility = results.Any() ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                SearchResultsList.Visibility = Visibility.Collapsed;
            }
        }

        

        private void AddFriendButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is User selectedUser)
            {
                // friend request
            }
        }
    }
}
