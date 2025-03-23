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

        public void SaveChanges(string password, bool isPrivate, string base64Image)
        {
            CurrentUser.Password = password;
            CurrentUser.PrivacyStatus = isPrivate;
            CurrentUser.ProfileImage = base64Image; // this is what gets stored in DB

            _profileService.UpdateUser(CurrentUser);
        }

    }
}
