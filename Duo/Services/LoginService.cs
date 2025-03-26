using Duo.Models;
using Duo.Repositories;
using System;

namespace Duo.Services
{
    public class LoginService
    {
        private readonly UserRepository _userRepository;

        public LoginService()
        {
            _userRepository = new UserRepository(App.userRepository.DataLink);
        }

        public bool AuthenticateUser(string username, string password)
        {
            return _userRepository.ValidateCredentials(username, password);
        }

        public User GetUserByCredentials(string username, string password)
        {
            if (AuthenticateUser(username, password))
            {
                var user = _userRepository.GetUserByUsername(username);
                //Console.WriteLine($"User {user.UserName} has logged in.");
                user.OnlineStatus = true;

                _userRepository.UpdateUser(user);

            }
            return _userRepository.GetUserByCredentials(username, password);
        }

        public void UpdateUserStatusOnLogout(User user)
        {
            user.OnlineStatus = false;
            user.LastActivityDate = DateTime.Now;
            _userRepository.UpdateUser(user);
        }
    }
}
