using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using exact.api.Controllers;
using exact.api.Model;
using exact.api.Model.Proxy;
using lavasim.business.Business;
using lavasim.common.Extension;
using lavasim.common.Model;
using lavasim.common.Model.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace lavasim.api.Controllers
{
    /// <summary>
    /// User Controller's
    /// </summary>
    [Route("api/user")]
    [Authorize(Roles = "user, company, admin")]
    [ModelUser]
    [ProducesResponseType(typeof(ErrorProxy), 500)]
    [ProducesResponseType(typeof(ErrorProxy), 400)]
    public class UserController : BaseController
    {
        private readonly UserBusiness _business;

        public UserController(UserBusiness business)
        {
            _business = business;
        }

        /// <summary>
        /// Get user information
        /// </summary>
        /// <remarks>
        /// About "type" property on proxy:
        ///
        ///     Type
        ///     {
        ///        App = 0,
        ///        Backoffice = 1
        ///     }
        ///
        /// </remarks>
        /// <param name="authorization"></param>
        /// <returns></returns>
        [HttpGet("me")]
        [SwaggerResponse(200, Type = typeof(UserInfoProxy), Description = "Get user information")]
        public Task<IActionResult> Get(UserToken user)
        {
            return RunDefaultAsync(async () =>
            {
                var proxy = await _business.GetUserInfo(user.Id.Value);
                return Ok(proxy);
            });
            
        }
        
        /// <summary>
        /// Send email to password
        /// </summary>
        /// <returns></returns>
        [HttpPost("tryreset")]
        [AllowAnonymous]
        public Task<IActionResult> ResetPassword([FromQuery] string email)
        {
            return RunDefaultAsync(async () =>
            {
                await _business.SendEmailResetPassword(email);
                return Ok();
            });
        }        
          
        /// <summary>
        /// Reset password
        /// </summary>
        /// <returns></returns>
        [HttpPost("reset")]
        [AllowAnonymous]
        public Task<IActionResult> ResetPassword(
            [FromQuery] string email, 
            [FromQuery] string password,
            [FromQuery] string code)
        {
            
            return RunDefaultAsync(async () =>
            {
                var token = await _business.ResetPassword(email, password, code);
               
                return Ok(new JwtTokenProxy
                {
                    Token = $"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}",
                    Expiration = token.ValidTo
                });
            });
            
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPost("password")]
        public Task<IActionResult> Reset([FromHeader] string authorization,
            string oldPassword,
            string newPassword)
        {
            
            return RunDefaultAsync(async () =>
            {
                await _business.ChangePassword(authorization, newPassword, oldPassword);
                return Ok();
            });
            
        }     
    }
}