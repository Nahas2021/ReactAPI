using ReactAPI.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactAPI.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<List<Register>> GetAllRegistersAsync();
        Task<Register> GetRegisterByIdAsync(int id);
        Task<Register> AddRegisterAsync(Register register);
        Task UpdateRegisterAsync(Register register);
        Task DeleteRegisterAsync(int id);
    }
}