using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Starware.DatingApp.Core.DTOs;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.SharedKernal.Common;

namespace Starware.DatingApp.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Register([FromBody] RegisterDto registerData)
        {
            return Ok(await this.accountService.Register(registerData));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Login([FromBody] LoginDto loginData)
        {
            return Ok(await this.accountService.Login(loginData));
        }
    }
}
