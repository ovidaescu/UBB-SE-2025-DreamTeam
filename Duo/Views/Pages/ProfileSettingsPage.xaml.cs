using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using System;
using Windows.Storage;
using Duo;
using Duo.ViewModels;
using Duo.Models;

namespace DuolingoNou.Views.Pages
{
    public sealed partial class ProfileSettingsPage : Page
    {
        private ProfileViewModel _viewModel;
        private string _selectedImagePath = "";
        private string _selectedImageBase64;

        public ProfileSettingsPage()
        {
            this.InitializeComponent();

            _viewModel = new ProfileViewModel();
            LoadUserDataIntoUI();
        }


        private async void LoadUserDataIntoUI()
        {
            if (_viewModel.CurrentUser != null)
            {
                UsernameInput.Text = _viewModel.CurrentUser.UserName;
                EmailInput.Text = _viewModel.CurrentUser.Email;
                PasswordInput.Password = _viewModel.CurrentUser.Password;
                PublicRadio.IsChecked = !_viewModel.CurrentUser.PrivacyStatus;
                PrivateRadio.IsChecked = _viewModel.CurrentUser.PrivacyStatus;

                if (!string.IsNullOrEmpty(_viewModel.CurrentUser.ProfileImage))
                {
                    byte[] imageBytes = Convert.FromBase64String(_viewModel.CurrentUser.ProfileImage);
                    using (var stream = new InMemoryRandomAccessStream())
                    {
                        using (var writer = new DataWriter(stream.GetOutputStreamAt(0)))
                        {
                            writer.WriteBytes(imageBytes);
                            await writer.StoreAsync();       
                            await writer.FlushAsync();       
                            writer.DetachStream();
                        }

                        BitmapImage bitmap = new BitmapImage();
                        await bitmap.SetSourceAsync(stream); 
                        ProfileImageBrush.ImageSource = bitmap;
                    }
                }

            }
        }

        private async void OnProfileImageClick(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpeg");
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            // Correctly get HWND from MainWindow
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainAppWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                using IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                BitmapImage bitmap = new BitmapImage();
                await bitmap.SetSourceAsync(stream);
                ProfileImageBrush.ImageSource = bitmap;

                using (DataReader reader = new DataReader(stream.GetInputStreamAt(0)))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    byte[] buffer = new byte[stream.Size];
                    reader.ReadBytes(buffer);
                    _selectedImageBase64 = Convert.ToBase64String(buffer); 
                }
            }
        }

        private void OnSaveChangesClick(object sender, RoutedEventArgs e)
        {
            string password = PasswordInput.Password;
            bool isPrivate = PrivateRadio.IsChecked == true;

            _viewModel.SaveChanges(password, isPrivate, _selectedImageBase64);


            ContentDialog dialog = new ContentDialog
            {
                Title = "Saved",
                Content = $"Changes saved successfully.",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            _ = dialog.ShowAsync();
        }


        private void ShowPasswordToggle_Checked(object sender, RoutedEventArgs e)
        {
            VisiblePasswordInput.Text = PasswordInput.Password;
            PasswordInput.Visibility = Visibility.Collapsed;
            VisiblePasswordInput.Visibility = Visibility.Visible;
        }

        private void ShowPasswordToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordInput.Password = VisiblePasswordInput.Text;
            PasswordInput.Visibility = Visibility.Visible;
            VisiblePasswordInput.Visibility = Visibility.Collapsed;
        }
    }
    }
