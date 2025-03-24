using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using DuolingoNou.Views.Pages;

namespace DuolingoNou.Views
{
    public sealed partial class ShellPage : Page
    {
        public ShellPage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(ProfileSettingsPage)); // Default page
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem)
            {
                switch (selectedItem.Tag)
                {
                    case "Settings":
                        ContentFrame.Navigate(typeof(ProfileSettingsPage));
                        break;
                    case "HomePage":
                        ContentFrame.Navigate(typeof(MainPage)); // create this later
                        break;
                        /*
                    case "Quiz":
                        ContentFrame.Navigate(typeof(QuizPage)); // create this later
                        break;*/
                }
            }
        }
    }
}
