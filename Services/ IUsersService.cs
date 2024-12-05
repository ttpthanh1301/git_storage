using TourWebsite.ViewModels;

namespace TourWebsite.Services
{
    public interface IUsersService
    {
        Task<string> Authenticate(LoginRequest request);

        Task<bool> Register(RegisterRequest request);
    }
}