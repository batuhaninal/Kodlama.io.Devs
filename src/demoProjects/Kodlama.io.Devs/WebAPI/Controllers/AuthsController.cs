using Application.Features.Auths.Command.Register;
using Application.Features.Auths.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto registerDto)
        {
            RegisterCommand command = new()
            {
                UserForRegisterDto = registerDto,
                IpAddress = GetIpAddress()
            };

            RegisteredDto result = await Mediator.Send(command);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            };

            Response.Cookies.Append("refreshtoken", refreshToken.Token, cookieOptions);
        }
    }
}
