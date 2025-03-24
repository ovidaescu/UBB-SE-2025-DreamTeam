using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Duo.Views.Pages; 

namespace Duo
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(LoginPage));
        }
    }
}