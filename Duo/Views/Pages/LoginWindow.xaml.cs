using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Duo.Views.Pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
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
    }
}