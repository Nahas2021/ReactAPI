using ReactAPI.Core.Interfaces;
using ReactAPI.Core.Models;
using ReactAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactAPI.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterService(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        public async Task<List<Register>> GetAllRegistersAsync()
        {
            return await _registerRepository.GetAllRegistersAsync();
        }

        public async Task<Register> GetRegisterByIdAsync(int id)
        {
            return await _registerRepository.GetRegisterByIdAsync(id);
        }

        public async Task<Register> AddRegisterAsync(Register register)
        {
            return await _registerRepository.AddRegisterAsync(register);
        }

        public async Task UpdateRegisterAsync(Register register)
        {
            await _registerRepository.UpdateRegisterAsync(register);
        }

        public async Task DeleteRegisterAsync(int id)
        {
            await _registerRepository.DeleteRegisterAsync(id);
        }
    }
}