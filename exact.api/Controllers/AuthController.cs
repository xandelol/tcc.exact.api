using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using exact.api.Data.Enum;
using exact.api.Model.Payload;
using exact.api.Model.Proxy;
using exact.business.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exact.api.Controllers
{
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly UserBusiness _business;

        public AuthController(UserBusiness business)
        {
            _business = business;
        }
    
        /// <summary>
        /// Login into system and return a jwt token
        /// </summary>
        /// <param name="user">User to be logged in</param>
        /// <returns><see cref="JwtTokenProxy" /> information</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public Task<IActionResult> Login([FromQuery] string user, [FromQuery] string password, [FromQuery] int type)
        {
            UserType userType;
            if(type == 1)
            {
                userType = UserType.Backoffice;
            }
            else
            {
                userType = UserType.App;
            }
            return RunDefaultAsync(async () =>
            {
                var token = await _business.GetJwtSecurityToken(user, password, userType);

                return Ok(new JwtTokenProxy
                {
                    Token = $"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}",
                    Expiration = token.ValidTo
                });
            });
        }
    }
}