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
    }
}