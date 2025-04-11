using Microsoft.EntityFrameworkCore;
using ReactAPI.Core.Interfaces;
using ReactAPI.Core.Models;
using ReactAPI.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactAPI.Infrastructure.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly EmployeeContext _context;

        public RegisterRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<List<Register>> GetAllRegistersAsync()
        {
            return await _context.Registers.ToListAsync();
        }

        public async Task<Register> GetRegisterByIdAsync(int id)
        {
            return await _context.Registers.FindAsync(id);
        }

        public async Task<Register> AddRegisterAsync(Register register)
        {
            _context.Registers.Add(register);
            await _context.SaveChangesAsync();
            return register;
        }

        public async Task UpdateRegisterAsync(Register register)
        {
            _context.Entry(register).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRegisterAsync(int id)
        {
            var register = await _context.Registers.FindAsync(id);
            if (register != null)
            {
                _context.Registers.Remove(register);
                await _context.SaveChangesAsync();
            }
        }
    }
}