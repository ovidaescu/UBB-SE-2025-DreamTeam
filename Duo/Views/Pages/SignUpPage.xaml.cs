using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Duo.ViewModels;
using Duo.Models;
using Duo.Services;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Duo.Views.Pages
{
    public sealed partial class SignUpPage : Page
    {
        public SignUpViewModel ViewModel { get; set; }
        public User NewUser { get; set; } = new User();

        public SignUpPage()
        {
            this.InitializeComponent();
            ViewModel = new SignUpViewModel();
            this.DataContext = ViewModel;
        }

        private async void OnCreateUserClick(object sender, RoutedEventArgs e)
        {
            NewUser.UserName = UsernameTextBox.Text;
            NewUser.Email = EmailTextBox.Text;
            NewUser.Password = PasswordBoxWithRevealMode.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (!IsValidUsername(NewUser.UserName))
            {
                UsernameValidationTextBlock.Text = "Username must be 5-20 characters and contain only letters, digits, or underscores.";
                return;
            }
            else
            {
                UsernameValidationTextBlock.Text = "";
            }

            if (await IsUsernameTaken(NewUser.UserName))
            {
                await ShowDialog("Username Taken", "This username is already in use. Please choose another.");
                return;
            }

            string passwordStrength = GetPasswordStrength(NewUser.Password);
            if (passwordStrength == "Weak")
            {
                await ShowDialog("Weak Password", "Password must be at least Medium strength. Include an uppercase letter, a special character, and a digit.");
                return;
            }

            if (NewUser.Password != confirmPassword)
            {
                ConfirmPasswordValidationTextBlock.Text = "Passwords do not match.";
                return;
            }
            else
            {
                ConfirmPasswordValidationTextBlock.Text = "";
            }

            ViewModel.CreateNewUser(NewUser);
            await ShowDialog("Account Created", "Your account has been successfully created!");
            Frame.Navigate(typeof(ShellPage));
        }

        private async Task<bool> IsUsernameTaken(string username)
        {
            try
            {
                // Call the ViewModel's async method and await the result
                return await ViewModel.IsUsernameTaken(username);
            }
            catch (Exception ex)
            {
                // Log error (you should use proper logging in production)
                Console.WriteLine($"Error checking username: {ex.Message}");

                // Return true as a fail-safe to prevent duplicate usernames
                // if there's an error checking availability
                return true;
            }
        }

        private bool IsValidUsername(string username)
        {
            return Regex.IsMatch(username, "^[A-Za-z0-9_]{5,20}$");
        }

        private bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, "^(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,15}$");
        }

        private void PasswordBoxWithRevealMode_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordStrengthTextBlock.Text = GetPasswordStrength(PasswordBoxWithRevealMode.Password);
        }

        private string GetPasswordStrength(string password)
        {
            if (password.Length < 6) return "Weak";
            if (Regex.IsMatch(password, "^(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&]).{6,15}$")) return "Strong";
            if (Regex.IsMatch(password, "^(?=.*[A-Z])|(?=.*\\d)|(?=.*[@$!%*?&]).{6,15}$")) return "Medium";
            return "Weak";
        }

        private async Task ShowDialog(string title, string content)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await dialog.ShowAsync();
        }

        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            PasswordBoxWithRevealMode.PasswordRevealMode = RevealModeCheckBox.IsChecked == true ? PasswordRevealMode.Visible : PasswordRevealMode.Hidden;
            ConfirmPasswordBox.PasswordRevealMode = RevealModeCheckBox.IsChecked == true ? PasswordRevealMode.Visible : PasswordRevealMode.Hidden;
        }

        private void NavigateToLoginPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}