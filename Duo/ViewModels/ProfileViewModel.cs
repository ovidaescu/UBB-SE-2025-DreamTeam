using System;
using Duo.Services;
using Duo.Models;

namespace Duo.ViewModels
{
    public class ProfileViewModel
    {
        public User NewUser { get; set; }
        private readonly ProfileService _profileService;

        public ProfileViewModel()
        {
            _profileService = new ProfileService();
            NewUser = new User();
        }

        public void CreateNewUser()
        {
            NewUser.DateJoined = DateTime.Now;
            _profileService.CreateUser(NewUser);
        }
    }
}