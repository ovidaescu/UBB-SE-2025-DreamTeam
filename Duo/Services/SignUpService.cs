﻿using Duo.Models;
using Duo.Repositories;
using System;
using System.Threading.Tasks;

namespace Duo.Services
{
    public class SignUpService
    {
        private readonly UserRepository _userRepository;

        public SignUpService()
        {
            _userRepository = new UserRepository(App.userRepository.DataLink);
        }

        public async Task<bool> IsUsernameTaken(string username)
        {
            try
            {
                var user = await Task.Run(() => _userRepository.GetUserByUsername(username));
                return user != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking username: {ex.Message}");
                return true; // Fail-safe: assume taken if error occurs
            }
        }

        public async Task<bool> RegisterUser(User user)
        {
            // Check if email exists
            if (await Task.Run(() => _userRepository.GetUserByEmail(user.Email)) != null)
                return false;

            // Check if username exists
            if (await IsUsernameTaken(user.UserName))
                return false;

            _userRepository.CreateUser(user);
            return true;
        }
    }
}

