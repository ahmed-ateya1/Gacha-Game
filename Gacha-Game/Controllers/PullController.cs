using Gacha_Game.Core.Dtos;
using Gacha_Game.Core.Dtos.SpinDto;
using Gacha_Game.Core.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gacha_Game.Controllers
{
    /// <summary>
    /// Controller responsible for handling gacha pull (spin) operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PullController(ISpinService _spinService) : ControllerBase
    {
        /// <summary>
        /// Performs a spin on the specified gacha banner and returns the pulled characters.
        /// </summary>
        /// <param name="request">The spin request containing the banner ID and pull type.</param>
        /// <returns>
        /// Returns an <see cref="ApiResponse"/> with the result of the spin.
        /// If successful, the response contains the pulled characters; otherwise, it contains an error message.
        /// </returns>
        /// <response code="200">Spin was successful. Returns the pulled characters.</response>
        /// <response code="400">Spin failed due to invalid input or other issues.</response>
        [HttpPost("spin")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> Spin([FromBody] SpinAddRequest request)
        {
            var result = await _spinService.SpinAsync(request);
            if (result == null)
            {
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Spin failed",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Spin successful",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }
    }
}
