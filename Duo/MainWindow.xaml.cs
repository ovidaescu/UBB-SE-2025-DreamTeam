using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Duo.Views;

namespace DuolingoNou
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(ProfileView));
        }
    }
}
