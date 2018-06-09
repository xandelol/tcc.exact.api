using System;
using System.Threading.Tasks;
using exact.api.Controllers;
using exact.api.Data.Model;
using exact.api.Model.Proxy;
using lavasim.common.Extension;
using lavasim.common.Model.Payload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using lavasim.business.Business;

namespace lavasim.api.Controllers
{
    /// <summary>
    ///     Controller for settings endpoints.
    /// </summary>
    [Produces("application/json")]
    [Route("api/setting")]
    [ModelUser]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Authorize(Roles = "admin")]
    public class SettingController : BaseController
    {
        private readonly SettingBusiness _settingBusiness;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="businesses"></param>
        public SettingController(SettingBusiness businesses)
        {
            _settingBusiness = businesses;
        }
        
        [ProducesResponseType(typeof(bool), 200)]
        [HttpPut("update")]
        public IActionResult Update([FromBody] SettingEntity payload)
        {
            return RunDefault(() => Ok(_settingBusiness.UpdateSetting(payload)));
        }
        
        [ProducesResponseType(typeof(SettingEntity), 200)]
        [HttpGet]
        public Task<IActionResult> Get([FromQuery] Guid id)
        {
            return RunDefaultAsync(async () => Ok( await _settingBusiness.Get(id)));
        }
        
        // GET
        [ProducesResponseType(typeof(DataTableProxy<SettingEntity> ), 200)]
        [HttpGet("list")]
        public IActionResult Get([FromBody] DataTablePayload payload)
        {
            return RunDefault(() => Ok(_settingBusiness.Get(payload)));
        }
        
    }
}