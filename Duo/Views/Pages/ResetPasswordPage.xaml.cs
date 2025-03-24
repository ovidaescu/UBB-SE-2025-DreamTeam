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

        private void OnResetPasswordClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewPassword = NewPasswordBox.Password;
            ViewModel.ConfirmPassword = ConfirmPasswordBox.Password;

            if (ViewModel.ResetPassword(ViewModel.NewPassword))
            {
                ViewModel.StatusMessage = "Password reset successfully!";
                // Optionally navigate back to login page
                Frame.Navigate(typeof(LoginPage));
            }
        }
    }
}
