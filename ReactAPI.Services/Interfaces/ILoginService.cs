using ReactAPI.Core.Models;
using System.Threading.Tasks;

namespace ReactAPI.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> AuthenticateAsync(LoginRequest loginRequest);
    }
}