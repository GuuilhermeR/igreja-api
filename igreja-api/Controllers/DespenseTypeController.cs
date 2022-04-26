using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nrmcontrolextension.IServices;
using nrmcontrolextension.Models;

namespace nrmcontrolapi.Controllers
{
    [Route("api/despense-type")]
    [Authorize]
    public class DespenseTypeController : ControllerBase
    {

        private readonly IDespenseTypeService _despenseTypeService;

        public DespenseTypeController(IDespenseTypeService despenseTypeService)
        {
            this._despenseTypeService = despenseTypeService;
        }

        [HttpGet()]
        [Route("{userId}")]
        public async Task<IActionResult> GetDespenseTypesByUser( string userId)
        {
            try
            {
                return Ok(await _despenseTypeService.GetDespenseTypesByUser(userId));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] DespenseType despenseType)
        {
            try
            {
                return Ok(await _despenseTypeService.InsertDespenseType(despenseType));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
