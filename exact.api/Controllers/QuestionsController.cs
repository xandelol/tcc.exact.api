using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using exact.api.Controllers;
using exact.api.Model;
using exact.api.Model.Payload;
using exact.api.Model.Proxy;
using exact.business.Business;
using exact.common.Extension;
using exact.common.Model.Payload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace exact.api.Controllers
{
    /// <summary>
    ///     Controller for questions endpoints.
    /// </summary>
    [Produces("application/json")]
    [Route("api/question")]
    [ModelUser]
    [AllowAnonymous]
    public class QuestionController : BaseController
    {
        private readonly QuestionBusiness _businesses;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="businesses"></param>
        public QuestionController(QuestionBusiness businesses)
        {
            _businesses = businesses;
        }

        /// <summary>
        ///     Gets a number of random questions that the user has not seen before.
        ///     If the user sees then all, the seen record is reset and all questions can
        ///     be selected again.
        /// </summary>
        /// <response code="200">Random questions.</response>
        /// <response code="500">
        ///     We messed up somenthing and we aren't sure what, no system is perfect, check message for more
        ///     information.
        /// </response>
        /// <returns></returns>

        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("create")]
        public IActionResult Create([FromBody] QuestionPayload payload)
        {
            return RunDefault(() => Ok(_businesses.Create(payload)));
        }

        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut("update")]
        public IActionResult Update([FromBody] QuestionPayload payload)
        {
            return RunDefault(() => Ok(_businesses.Update(payload)));
        }

        [ProducesResponseType(typeof(QuestionProxy), 200)]
        [HttpGet]
        public IActionResult Get([FromQuery] Guid id)
        {
            return RunDefault(() => Ok(_businesses.Get(id)));
        }

        [ProducesResponseType(typeof(DataTableProxy<QuestionProxy>), 200)]
        [HttpGet("list")]
        public IActionResult List([FromBody] DataTablePayload payload)
        {
            return RunDefault(() => Ok(_businesses.Get(payload)));
        }

        [ProducesResponseType(typeof(List<QuestionProxy>),200)]
        [AllowAnonymous]
        [HttpGet("game/list")]
        public Task<IActionResult> List() {
            return RunDefaultAsync(async () => Ok(await _businesses.Get()));
        }
    }
}