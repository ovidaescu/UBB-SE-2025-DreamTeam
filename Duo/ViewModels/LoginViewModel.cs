using Duo.Services;
using Microsoft.UI.Xaml;

namespace Duo.ViewModels
{
    public class LoginViewModel
    {
        private readonly LoginService _loginService;

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool LoginStatus { get; private set; }

        public LoginViewModel()
        {
            _loginService = new LoginService();
        }

        public void AttemptLogin(string username, string password)
        {
            Username = username;
            Password = password;
            LoginStatus = _loginService.AuthenticateUser(Username, Password);
        }

    }
}
