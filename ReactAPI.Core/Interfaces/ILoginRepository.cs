using ReactAPI.Core.Models;
using System.Threading.Tasks;

namespace ReactAPI.Core.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> AuthenticateAsync(LoginRequest loginRequest);
    }
}