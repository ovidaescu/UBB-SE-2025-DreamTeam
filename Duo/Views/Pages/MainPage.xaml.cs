using Duo.Repositories;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Duo.Models;
using Duo;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DuolingoNou.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private readonly UserRepository _userRepository;
        private List<User> _users;
        private List<User> _suggestedFriends;

        public MainPage()
        {
            this.InitializeComponent();
            _userRepository = App.userRepository;
            LoadUsers();
            GetFriendSuggestions();
        }

        private void LoadUsers()
        {
            _users = _userRepository.GetAllUsers();
        }

        private void GetFriendSuggestions()
        {
            int currentUserId = _users.First().UserId;
            var userFriends = _userRepository.GetUserFriends(currentUserId);

            _suggestedFriends = new List<User>();

            foreach (var friend in userFriends)
            {
                var mutualFriends = _userRepository.GetUserFriends(friend.UserId)
                    .Where(f => f.UserId != currentUserId && !userFriends.Contains(f)) // exclude current user and already added friends
                    .ToList();

                _suggestedFriends.AddRange(mutualFriends);
            }

            _suggestedFriends = _suggestedFriends.Distinct().ToList();
            SuggestedFriendsGridView.ItemsSource = _suggestedFriends;
        }
    }
}
