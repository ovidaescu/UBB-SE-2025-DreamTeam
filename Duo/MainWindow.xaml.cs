using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Duo.Views.Pages;
using Duo.ViewModels;

namespace Duo
{
    public sealed partial class MainWindow : Window
    {
        private readonly LoginViewModel _loginViewModel;

        public MainWindow()
        {
            this.InitializeComponent();
            _loginViewModel = new LoginViewModel();
            // this.DataContext = _loginViewModel;
            MainFrame.Navigate(typeof(LoginPage));
            this.Closed += MainWindow_Closed; // Handle the Closed event
        }


        private void MainWindow_Closed(object sender, WindowEventArgs e)
        {
            if (App.CurrentUser != null)
            {
                App.CurrentUser.OnlineStatus = false;
                App.CurrentUser.LastActivityDate = System.DateTime.Now;
                App.userRepository.UpdateUser(App.CurrentUser);
                //_loginViewModel.UpdateUserStatusOnLogout(App.CurrentUser);
                // write current user
                System.Diagnostics.Debug.WriteLine(App.CurrentUser.UserName + App.CurrentUser.UserId + " has logged out.");
            }

        }
    }
}