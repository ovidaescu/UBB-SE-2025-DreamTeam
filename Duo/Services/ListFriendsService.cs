using Duo;
using Duo.Models;
using Duo.Repositories;
using Duo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Duo.Services
{
    public class FriendsService
    {
        private readonly ListFriendsRepository _friendRepository;

        public FriendsService()
        {
            _friendRepository = new ListFriendsRepository(new UserRepository(App.userRepository.DataLink));
        }

        // Fetch the list of friends for a specific user
        public List<User> GetFriends(int userId)
        {
            return _friendRepository.GetFriends(userId);
        }

        // Sort friends by name (alphabetically)
        public List<User> SortFriendsByName(int userId)
        {
            return _friendRepository.SortFriendsByName(userId);
        }

        // Sort friends by the date they joined (ascending)
        public List<User> SortFriendsByDateAdded(int userId)
        {
            return _friendRepository.SortFriendsByDateAdded(userId);
        }


        public List<User> SortFriendsByOnlineStatus(int userId)
        {
            return _friendRepository.SortFriendsByOnlineStatus(userId);
        }
    }
}
