using Duo.Models;
using Duo.Services;

namespace Duo.ViewModels
{
    public class ProfileViewModel
    {
        private readonly ProfileService _profileService;

        public User CurrentUser { get; set; }

        public ProfileViewModel(User user)
        {
            _profileService = new ProfileService();
            CurrentUser = user;
        }

        public ProfileViewModel()
        {
            _profileService = new ProfileService();
            CurrentUser = App.CurrentUser;
        }


        public void SaveChanges(string password, bool isPrivate, string newBase64Image)
        {
            CurrentUser.Password = password;

            // Only update if a new image is provided
            if (!string.IsNullOrWhiteSpace(newBase64Image))
            {
                CurrentUser.ProfileImage = newBase64Image;
            }

            CurrentUser.PrivacyStatus = isPrivate;

            _profileService.UpdateUser(CurrentUser);
        }


    }
}
