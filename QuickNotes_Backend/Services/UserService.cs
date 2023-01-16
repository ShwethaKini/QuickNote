using QuickNotes.Models;

namespace QuickNotes.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users;
        public UserService()
        {
            _users = new List<User>();
            _users.Add(new User()
            {
                Username ="admin"
            });
            _users.Add(new User()
            {
                Username = "user"
            });

        }
        public async Task<User?> Authenticate(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            if(username == "admin" && password == "admin" || username == "user" && password == "user")
            {
                return await Task.FromResult( new User()
                {
                    Username = username,

                });

            } else
            {
                return null;
            }
        }
    }
}
