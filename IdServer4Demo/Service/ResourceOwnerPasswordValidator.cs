using IdentityServer4.Models;
using IdentityServer4.Validation;
using IdServer4Demo.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdServer4Demo.Service
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;
        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _userRepository.GetUserByUid(context.UserName, context.Password).Result;
            
            if (user != null)
            {
                context.Result = new GrantValidationResult(user.UserUid, "user");
                return Task.FromResult(context.Result);
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User not found.", null);
            return Task.FromResult(context.Result);
        }
    }
}
