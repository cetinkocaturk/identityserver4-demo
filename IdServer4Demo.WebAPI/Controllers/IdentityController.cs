using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using IdentityModel.Client;
using System.Net.Http;
using System.Security.Claims;
using IdServer4Demo.WebAPI.Models.RequestModel;
using IdentityModel;
using Newtonsoft.Json;
using IdServer4Demo.WebAPI.Models.ResponseModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace IdServer4Demo.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public IdentityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("create-token")]
        public async Task<IActionResult> CreateToken()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_configuration["IdentityServer:BaseUrl"]);

            if (disco.IsError)
            {
                return BadRequest(disco.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "jwtSampleApi",
            });

            if (tokenResponse.IsError)
            {
                return BadRequest(tokenResponse.Error);
            }

            var jsonResponse = tokenResponse.Json;

            return Ok(tokenResponse.AccessToken);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(UserLoginRequest request)
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_configuration["IdentityServer:BaseUrl"]);

            if (disco.IsError)
            {
                return Ok(disco.Error);
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                GrantType = "password",
                ClientSecret = "secret",
                Scope = "jwtSampleApi",
                UserName = request.UserId,
                Password = request.Password
            });

            if (tokenResponse.IsError)
            {
                return BadRequest(tokenResponse.Error);
            }

            var response = JsonConvert.DeserializeObject<UserLoginResponse>(tokenResponse.Json.ToString());

            return Ok(response);
        }

        [Authorize]
        [HttpGet("try-token")]
        public IActionResult TryToken()
        {
            var userClaims = User.Claims.Select(s => new { Type = s.Type, Value = s.Value });

            return Ok(userClaims);
        }
    }

}