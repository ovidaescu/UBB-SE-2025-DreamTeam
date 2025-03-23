using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using DuolingoNou.Views.Pages; // Corrected namespace for Pages

namespace DuolingoNou
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(ProfileSettingsPage));
        }
    }
}