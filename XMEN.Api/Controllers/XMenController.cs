using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XMEN.Api.Responses;
using XMEN.Core.DTOs;
using XMEN.Core.Interfaces;

namespace XMEN.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class XMenController : ControllerBase
    {
        private readonly IMutantService _mutanService;

        public XMenController(IMutantService mutantService)
        {
            _mutanService = mutantService;
        }

        [HttpGet]
        public async Task<IActionResult> Statistics()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Mutant(MutantRequest mutantRequest)
        {
            bool result = _mutanService.IsMutant(mutantRequest);
            return Ok(new ApiResponse<bool>(result));
        }

    }
}
