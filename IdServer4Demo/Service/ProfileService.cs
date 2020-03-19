using IdentityServer4.Models;
using IdentityServer4.Services;
using IdServer4Demo.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdServer4Demo.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;
        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userUid = context.Subject.Claims.First().Value;
            var user = _userRepository.GetUserByEmail(userUid).Result;

            var claimList = new List<Claim>()
            {
                new Claim("UserId",user.UserId,ClaimValueTypes.Integer),
                new Claim("UserUid",user.UserUid, ClaimValueTypes.String),
                new Claim("UserPassword",user.UserPassword,ClaimValueTypes.String),
                new Claim("IsActive",user.IsActive.ToString(),ClaimValueTypes.Boolean)
            };

            context.IssuedClaims = claimList;
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var userUid = context.Subject.Claims.First().Value;
            var user = _userRepository.GetUserByEmail(userUid).Result;
            context.IsActive = user?.IsActive ?? false;
            return Task.FromResult(0);
        }
    }
}
