using Duo.Services;
using Duo;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Duo.Models;
using Duo.UI.ViewModels;
using Windows.Storage.Streams;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DuolingoNou.Views.Pages
{

    public sealed partial class MainPage : Page
    {
        private readonly ProfileService _profileService;
        public ListFriendsViewModel ViewModel { get; }

        public MainPage()
        {
            this.InitializeComponent();
            _profileService = new ProfileService();
            LoadUserDetails();
            ViewModel = new ListFriendsViewModel(); // Initialize ViewModel
            this.DataContext = ViewModel; // Set DataContext for binding
        }

        private BitmapImage ConvertBase64ToImage(string base64String)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);
                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    using (DataWriter writer = new DataWriter(stream))
                    {
                        writer.WriteBytes(bytes);
                        writer.StoreAsync().AsTask().Wait();
                    }

                    BitmapImage image = new BitmapImage();
                    stream.Seek(0);
                    image.SetSource(stream);
                    return image;
                }
            }
            catch
            {
                // Return a default image if conversion fails
                return new BitmapImage(new Uri("ms-appx:///Assets/default_profile.png"));
            }
        }

        private void LoadUserDetails()
        {
            User currentUser = _profileService.GetUserStats(App.CurrentUser.UserId);

            if (currentUser != null)
            {
                UsernameText.Text = currentUser.UserName;
                FriendCountText.Text = $"18 friends";
                if (!string.IsNullOrEmpty(currentUser.ProfileImage))
                {
                    if (currentUser.ProfileImage.StartsWith("data:image")) // If it's Base64
                    {
                        ProfilePic.ImageSource = ConvertBase64ToImage(currentUser.ProfileImage.Split(',')[1]); // Remove metadata
                    }
                    else if (Uri.IsWellFormedUriString(currentUser.ProfileImage, UriKind.Absolute)) // If it's a valid URL
                    {
                        ProfilePic.ImageSource = new BitmapImage(new Uri(currentUser.ProfileImage));
                    }
                    else
                    {
                        // Handle invalid image path (fallback to default image)
                        ProfilePic.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/default_profile.png"));
                    }
                }

                // Update statistics
                DayStreakText.Text = currentUser.Streak.ToString();
                TotalXPText.Text = currentUser.TotalPoints.ToString();
                QuizzesCompletedText.Text = currentUser.QuizzesCompleted.ToString();
                CoursesCompletedText.Text = currentUser.CoursesCompleted.ToString();

                // Award achievements
                _profileService.AwardAchievements(currentUser);
                System.Diagnostics.Debug.WriteLine("AwardAchievements called");

            }
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                var selectedSortOption = selectedItem.Content.ToString();

                // Sort based on the selected option
                if (selectedSortOption == "Sort By Name")
                {
                    ViewModel.SortByName();
                }
                else if (selectedSortOption == "Sort By Date Added")
                {
                    ViewModel.SortByDateAdded();
                }
                else if (selectedSortOption == "Sort By Activity")
                {
                    ViewModel.SortByOnlineStatus();
                }
            }
        }

        private void OnProfileImageClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProfileSettingsPage));
        }

        private void OnUpdateProfileClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProfileSettingsPage));
        }

        private void OnViewAllClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AchievementsPage));
        }
    }
}
