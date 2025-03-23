using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Duo.ViewModels;

namespace DuolingoNou.Views.Pages
{
    public sealed partial class SignUpPage : Page
    {
        public SignUpViewModel ViewModel { get; set; }

        public SignUpPage()
        {
            this.InitializeComponent();
            ViewModel = new SignUpViewModel();
            this.DataContext = ViewModel;
        }

        private void OnCreateUserClick(object sender, RoutedEventArgs e)
        {
            ViewModel.CreateNewUser();
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

        private void NavigateToLoginPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}