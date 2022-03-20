using System.Threading.Tasks;
using TestLoginApp.Models;

namespace TestLoginApp.Repository
{
    public interface IRestService
    {
        Task<HttpResponseLoginModel> LoginAsync(string login, string password);

        Task<bool> LogoutAsync(string token);

        Task<HttpResponseProfileModel> GetUserProfileAsync(string token);
    }
}
