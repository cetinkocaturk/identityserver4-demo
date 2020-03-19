using IdServer4Demo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdServer4Demo.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUid(string uid, string password);
        Task<User> GetUserByEmail(string email);
    }
}
