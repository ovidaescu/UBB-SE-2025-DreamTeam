using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Duo.ViewModels;

namespace Duo.Views
{
    public sealed partial class ProfileView : Page
    {
        public ProfileViewModel ViewModel { get; set; }

        public ProfileView()
        {
            this.InitializeComponent();
            ViewModel = new ProfileViewModel();
            this.DataContext = ViewModel;
        }

        private void OnCreateUserClick(object sender, RoutedEventArgs e)
        {
            ViewModel.CreateNewUser();
        }
    }
}