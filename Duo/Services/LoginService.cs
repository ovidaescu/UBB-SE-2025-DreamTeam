using Duo.Repositories;

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
    }
}
