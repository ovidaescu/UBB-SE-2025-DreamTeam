using System;
using Duo.Services;
using Duo.Models;

namespace Duo.ViewModels
{
    public class SignUpViewModel
    {
        public User NewUser { get; set; }
        private readonly SignUpService _signUpService;

        public SignUpViewModel()
        {
            _signUpService = new SignUpService();
            NewUser = new User();
        }

        public void CreateNewUser()
        {
            NewUser.DateJoined = DateTime.Now;
            _signUpService.RegisterUser(NewUser);
        }
    }
}