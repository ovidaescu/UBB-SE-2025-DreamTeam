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
using Duo.ViewModels;
using Duo;
using Duo.Views.Pages;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace DuolingoNou.Views.Pages
{

    public sealed partial class ResetPasswordPage : Page
    {
        public ResetPassViewModel ViewModel { get; set; }

        public ResetPasswordPage()
        {
            this.InitializeComponent();
            ViewModel = new ResetPassViewModel(App.userRepository);
            this.DataContext = ViewModel;
        }

        private async void OnSendCodeClick(object sender, RoutedEventArgs e)
        {
            bool success = await ViewModel.SendVerificationCode(ViewModel.Email);
            if (success)
            {
                EmailPanel.Visibility = Visibility.Collapsed;
                CodePanel.Visibility = Visibility.Visible;
                ViewModel.StatusMessage = "Verification code sent to your email!";
            }
            else
            {
                ViewModel.StatusMessage = "Email not found or error sending code.";
            }
        }

        private void OnVerifyCodeClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel.VerifyCode(ViewModel.VerificationCode))
            {
                CodePanel.Visibility = Visibility.Collapsed;
                PasswordPanel.Visibility = Visibility.Visible;
                ViewModel.StatusMessage = "Code verified! Enter your new password.";
            }
            else
            {
                ViewModel.StatusMessage = "Invalid verification code!";
            }
        }

        private string GetPasswordStrength(string password)
        {
            if (password.Length < 6) return "Weak";
            if (Regex.IsMatch(password, "^(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&]).{6,15}$")) return "Strong";
            if (Regex.IsMatch(password, "^(?=.*[A-Z])|(?=.*\\d)|(?=.*[@$!%*?&]).{6,15}$")) return "Medium";
            return "Weak";
        }

        private void PasswordBoxWithRevealMode_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordStrengthTextBlock.Text = GetPasswordStrength(PasswordBoxWithRevealMode.Password);
        }

        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            PasswordBoxWithRevealMode.PasswordRevealMode = RevealModeCheckBox.IsChecked == true ? PasswordRevealMode.Visible : PasswordRevealMode.Hidden;
            ConfirmPasswordBox.PasswordRevealMode = RevealModeCheckBox.IsChecked == true ? PasswordRevealMode.Visible : PasswordRevealMode.Hidden;
        }

        private void OnResetPasswordClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewPassword = PasswordBoxWithRevealMode.Password;
            ViewModel.ConfirmPassword = ConfirmPasswordBox.Password;
            string passwordStrength = GetPasswordStrength(PasswordBoxWithRevealMode.Password);
            if (passwordStrength == "Weak")
            {
                ShowDialog("Weak Password", "Password must be at least Medium strength. Include an uppercase letter, a special character, and a digit.");
                return;
            }
            if (ViewModel.ResetPassword(ViewModel.NewPassword))
            {
                ViewModel.StatusMessage = "Password reset successfully!";
                // Optionally navigate back to login page
                Frame.Navigate(typeof(LoginPage));
            }
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
    }
}