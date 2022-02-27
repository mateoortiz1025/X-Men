using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        public async Task<IActionResult> Stats()
        {
            var response = await _mutanService.GetStatistics();
            return Ok(new ApiResponse<StatisticsResponse>(response));
        }

        [HttpPost]
        public async Task<IActionResult> Mutant(MutantRequest mutantRequest)
        {
            bool isMutant = await _mutanService.IsMutant(mutantRequest);
            if (isMutant)
            {
                return Ok(new ApiResponse<bool>(isMutant));
            }

            return new ObjectResult(new ApiResponse<bool>(isMutant)) { StatusCode = (int)HttpStatusCode.Forbidden };
        }

    }
}
