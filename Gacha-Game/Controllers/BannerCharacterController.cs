using Gacha_Game.Core.CQRS.Command.BannerCharacterCommand;
using Gacha_Game.Core.CQRS.Queries.BannerCharacterQueries;
using Gacha_Game.Core.Dtos;
using Gacha_Game.Core.Dtos.BannerCharacterDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gacha_Game.Controllers
{
    /// <summary>
    /// Controller for managing banner character operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BannerCharacterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BannerCharacterController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BannerCharacterController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR mediator for handling commands and queries.</param>
        /// <param name="logger">The logger for logging controller actions.</param>
        public BannerCharacterController(IMediator mediator, ILogger<BannerCharacterController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new banner character.
        /// </summary>
        /// <param name="request">The request containing details for the new banner character.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        /// <response code="200">Returns the created banner character details if successful.</response>
        /// <response code="400">Returns an error message if the banner character could not be added.</response>
        [HttpPost("addBannerCharacter")]
        public async Task<ActionResult<ApiResponse>> AddBannerCharacter([FromBody] BannerCharacterAddRequest request)
        {
            var result = await _mediator.Send(new CreateBannerCharacterCommand(request));
            if (result == null)
            {
                _logger.LogError("Failed to add banner character");
                return BadRequest(new ApiResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = "Failed to add banner character."
                });
            }
            _logger.LogInformation("Banner character added successfully");
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Banner character added successfully.",
                Result = result
            });
        }

        /// <summary>
        /// Updates an existing banner character.
        /// </summary>
        /// <param name="request">The request containing updated details for the banner character.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        /// <response code="200">Returns the updated banner character details if successful.</response>
        /// <response code="404">Returns an error message if the banner character was not found.</response>
        [HttpPut("updateBannerCharacter")]
        public async Task<ActionResult<ApiResponse>> UpdateBannerCharacter([FromBody] BannerCharacterUpdateRequest request)
        {
            var result = await _mediator.Send(new UpdateBannerCharacterCommand(request));
            if (result == null)
            {
                _logger.LogError("Failed to update banner character: not found");
                return NotFound(new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    Message = "Banner character not found."
                });
            }
            _logger.LogInformation("Banner character updated successfully");
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Banner character updated successfully.",
                Result = result
            });
        }

        /// <summary>
        /// Retrieves banner characters by banner ID.
        /// </summary>
        /// <param name="bannerId">The unique identifier of the banner.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the banner characters.</returns>
        /// <response code="200">Returns the banner characters if found.</response>
        /// <response code="404">Returns an error message if no banner characters were found.</response>
        [HttpGet("getBannerCharacter/{bannerId}")]
        public async Task<ActionResult<ApiResponse>> GetBannerCharacter(Guid bannerId)
        {
            var result = await _mediator.Send(new GetAllBannerCharacterQuery(x => x.BannerId == bannerId));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve banner characters for banner ID {BannerId}", bannerId);
                return NotFound(new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    Message = "Banner character not found."
                });
            }
            _logger.LogInformation("Banner characters retrieved successfully for banner ID {BannerId}", bannerId);
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Banner character retrieved successfully.",
                Result = result
            });
        }

        /// <summary>
        /// Retrieves all banner characters.
        /// </summary>
        /// <returns>An <see cref="ApiResponse"/> containing a list of all banner characters.</returns>
        /// <response code="200">Returns the list of banner characters if successful.</response>
        /// <response code="404">Returns an error message if no banner characters were found.</response>
        [HttpGet("getAllBannerCharacter")]
        public async Task<ActionResult<ApiResponse>> GetAllBannerCharacter()
        {
            var result = await _mediator.Send(new GetAllBannerCharacterQuery());
            if (result == null)
            {
                _logger.LogError("Failed to retrieve all banner characters");
                return NotFound(new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    Message = "No banner characters found."
                });
            }
            _logger.LogInformation("All banner characters retrieved successfully");
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "All banner characters retrieved successfully.",
                Result = result
            });
        }

        /// <summary>
        /// Retrieves a banner character by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the banner character.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the banner character details.</returns>
        /// <response code="200">Returns the banner character if found.</response>
        /// <response code="404">Returns an error message if the banner character was not found.</response>
        [HttpGet("getBannerCharacterById/{id}")]
        public async Task<ActionResult<ApiResponse>> GetBannerCharacterById(Guid id)
        {
            var result = await _mediator.Send(new GetByBannerCharacterQuery(x => x.Id == id));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve banner character with ID {Id}", id);
                return NotFound(new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    Message = "Banner character not found."
                });
            }
            _logger.LogInformation("Banner character retrieved successfully with ID {Id}", id);
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Banner character retrieved successfully.",
                Result = result
            });
        }
    }
}