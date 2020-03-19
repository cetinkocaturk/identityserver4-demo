using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdServer4Demo.WebAPI.Models.RequestModel
{
    public class UserLoginRequest
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
