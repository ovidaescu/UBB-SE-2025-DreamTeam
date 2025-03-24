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
            // Ensure the password from PasswordBox is assigned before sending to DB
            ViewModel.NewUser.Password = PasswordBoxWithRevealMode.Password;

            // Call ViewModel logic
            ViewModel.CreateNewUser();

            // Optionally set the CurrentUser globally if needed
            Duo.App.CurrentUser = ViewModel.NewUser;

            // Navigate to app shell
            Frame.Navigate(typeof(ShellPage));
        }

        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            PasswordBoxWithRevealMode.PasswordRevealMode =
                RevealModeCheckBox.IsChecked == true ?
                PasswordRevealMode.Visible :
                PasswordRevealMode.Hidden;
        }

        private void NavigateToLoginPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
