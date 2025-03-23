using Duo.Models;
using Duo.Repositories;
using Duo;

namespace Duo.Services
{
    public class ProfileService
    {
        private readonly UserRepository _userRepository;

        public ProfileService()
        {
            _userRepository = new UserRepository(App.userRepository.DataLink);
        }
    }
}