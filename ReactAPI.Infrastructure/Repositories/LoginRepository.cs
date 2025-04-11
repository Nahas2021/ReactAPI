﻿using Microsoft.EntityFrameworkCore;
using ReactAPI.Core.Interfaces;
using ReactAPI.Core.Models;
using ReactAPI.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace ReactAPI.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly EmployeeContext _context;

        public LoginRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<string> AuthenticateAsync(LoginRequest loginRequest)
        {
            var user = await _context.Registers
                .FirstOrDefaultAsync(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);

            if (user == null)
            {
                return "Invalid username or password.";
            }

            return "Login successful!";
        }
    }
}