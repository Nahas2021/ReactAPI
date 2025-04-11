using ReactAPI.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactAPI.Core.Interfaces
{
    public interface IRegisterRepository
    {
        Task<List<Register>> GetAllRegistersAsync();
        Task<Register> GetRegisterByIdAsync(int id);
        Task<Register> AddRegisterAsync(Register register);
        Task UpdateRegisterAsync(Register register);
        Task DeleteRegisterAsync(int id);
    }
}