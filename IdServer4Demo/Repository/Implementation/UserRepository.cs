using IdServer4Demo.Models;
using IdServer4Demo.Models.Entities;
using IdServer4Demo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdServer4Demo.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _applicationContext;
        public UserRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _applicationContext.Users.SingleOrDefaultAsync(s => s.UserUid == email);
        }

        public async Task<User> GetUserByUid(string uid, string password)
        {
            return await _applicationContext.Users.SingleOrDefaultAsync(s => s.UserUid == uid && s.UserPassword == password);
        }
    }
}
