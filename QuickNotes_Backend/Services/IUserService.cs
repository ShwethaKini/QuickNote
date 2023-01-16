using QuickNotes.Models;

namespace QuickNotes.Services
{
    public interface IUserService
    {
        Task<User?> Authenticate(string username, string password);
    }
}
