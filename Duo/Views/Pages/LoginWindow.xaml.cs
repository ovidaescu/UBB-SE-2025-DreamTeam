using Duo;
using Duo.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace Duo.Views.Pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel { get; set; }
        public LoginPage()
        {
            this.InitializeComponent();
            ViewModel = new LoginViewModel();
            this.DataContext = ViewModel;
        }

        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            if (RevealModeCheckBox.IsChecked == true)
            {
                PasswordBoxWithRevealMode.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                PasswordBoxWithRevealMode.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }
        private void NavigateToSignUpPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUpPage));
        }

        private void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBoxWithRevealMode.Password; 
            Console.WriteLine(username);
            ViewModel.AttemptLogin(username,password);
            if (ViewModel.LoginStatus)
            {
                App.CurrentUser = ViewModel.LoggedInUser;
                LoginStatusMessage.Text = "You have successfully logged in!";
                LoginStatusMessage.Visibility = Visibility.Visible;
                Frame.Navigate(typeof(ShellPage));
            }
            else
            {
                LoginStatusMessage.Text = "Invalid username or password.";
                LoginStatusMessage.Visibility = Visibility.Visible;
            }
        }
    }
}