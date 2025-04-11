using ReactAPI.Core.Interfaces;
using ReactAPI.Core.Models;
using ReactAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace ReactAPI.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<string> AuthenticateAsync(LoginRequest loginRequest)
        {
            return await _loginRepository.AuthenticateAsync(loginRequest);
        }
    }
}