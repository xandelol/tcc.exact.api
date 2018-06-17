using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using exact.api.Model;
using exact.common.Model;
using Microsoft.AspNetCore.Mvc.Filters;

namespace exact.common.Extension
{
    public class ModelUserAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///     Gets user information
        /// </summary>
        /// <param name="context">Context</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var keyValuePairs = context.ActionArguments.Where(y => y.Value is UserToken).ToList();

            if (!keyValuePairs.Any()) return;

            if (context.HttpContext.Request.Headers.All(a => a.Key != "Authorization"))
            {
                foreach (var pair in keyValuePairs)
                    context.ActionArguments[pair.Key] = new UserToken();
                
                return;
            }
                

            var substring = context.HttpContext.Request.Headers["Authorization"][0].Substring(7);

            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(substring);
            var id = Guid.Parse(jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value);
            var roles = jwtSecurityToken.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);

            Guid? companyId = null;

            if (jwtSecurityToken.Claims.Any(x => x.Type == ClaimTypes.Sid))
            {
                companyId = Guid.Parse(jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value);
            }

            foreach (var pair in keyValuePairs)
                context.ActionArguments[pair.Key] = new UserToken
                {
                    Id = id,
                    Roles = roles
                };
        }
    }
}