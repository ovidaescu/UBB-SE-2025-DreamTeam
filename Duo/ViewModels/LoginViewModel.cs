using Duo.Services;
using Duo.Models;

namespace Duo.ViewModels
{
    public class LoginViewModel
    {
        private readonly LoginService _loginService;

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool LoginStatus { get; private set; }

        public User LoggedInUser { get; private set; }  // <-- Add this

        public LoginViewModel()
        {
            _loginService = new LoginService();
        }

        public void AttemptLogin(string username, string password)
        {
            Username = username;
            Password = password;

            // Try to get the user (instead of just bool result)
            LoggedInUser = _loginService.GetUserByCredentials(Username, Password);

            LoginStatus = LoggedInUser != null;
        }
    }
}
