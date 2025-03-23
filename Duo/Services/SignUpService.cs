using Duo.Models;
using Duo.Repositories;
using System;

namespace Duo.Services
{
    public class SignUpService
    {
        private readonly UserRepository _userRepository;

        public SignUpService()
        {
            _userRepository = new UserRepository(App.userRepository.DataLink);
        }

        public bool RegisterUser(User user)
        {
            if (_userRepository.GetUserByEmail(user.Email) != null)
                return false; 

            if (_userRepository.GetUserByUsername(user.UserName) != null)
                return false; 


            _userRepository.CreateUser(user);
            return true;
        }
    }
}
