using System;
using System.Threading.Tasks;
using exact.api.Exception;
using lavasim.common.Model.Proxy;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace exact.api.Controllers
{
    public class BaseController : Microsoft.AspNetCore.Mvc.Controller
    {

        public T GetProxy<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        ///     Run <paramref name="predicate" /> under default statement.
        /// </summary>
        /// <param name="predicate">Function to be ran.</param>
        /// <returns><paramref name="predicate" /> return or default return in case of an error has been thrown.</returns>
        protected async Task<IActionResult> RunDefaultAsync(Func<Task<IActionResult>> predicate)
        {
            try
            {
                return await predicate();
            }
            catch (InvalidArgumentException invalidArgumentException)
            {
                return BadRequest(new ErrorProxy
                {
                    Code = 3,
                    Message = invalidArgumentException.Message
                });
            }
            catch (TokenInvalidException tokenExcetion)
            {
                return StatusCode(401, new ErrorProxy
                {
                    Code = 0,
                    Message = tokenExcetion.Message
                });
            }
            catch (System.Exception exception)
            {
                return StatusCode(500, new ErrorProxy
                {
                    Code = 0,
                    Message = exception.Message
                });
            }
        }

        /// <summary>
        ///     Run <paramref name="predicate" /> under default statement.
        /// </summary>
        /// <param name="predicate">Function to be ran.</param>
        /// <returns><paramref name="predicate" /> return or default return in case of an error has been thrown.</returns>
        protected IActionResult RunDefault(Func<IActionResult> predicate)
        {
            try
            {
                return predicate();
            }
            catch (InvalidArgumentException invalidArgumentException)
            {
                return BadRequest(new ErrorProxy
                {
                    Code = 3,
                    Message = invalidArgumentException.Message
                });
            }
            catch (TokenInvalidException tokenExcetion)
            {
                return StatusCode(401, new ErrorProxy
                {
                    Code = 0,
                    Message = tokenExcetion.Message
                });
            }
            catch (System.Exception exception)
            {
                return StatusCode(500, new ErrorProxy
                {
                    Code = 0,
                    Message = exception.Message
                });
            }
        }

    }
}
