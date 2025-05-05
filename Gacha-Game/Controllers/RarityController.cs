using Gacha_Game.Core.CQRS.Command.RarityCommand;
using Gacha_Game.Core.CQRS.Queries.RarityQueries;
using Gacha_Game.Core.Dtos;
using Gacha_Game.Core.Dtos.RarityDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gacha_Game.Controllers
{
    /// <summary>
    /// Controller for managing rarity-related operations in the Gacha Game API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RarityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RarityController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RarityController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR instance for handling CQRS commands and queries.</param>
        /// <param name="logger">The logger instance for logging controller actions.</param>
        public RarityController(IMediator mediator, ILogger<RarityController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Adds a new rarity to the system.
        /// </summary>
        /// <param name="request">The request containing rarity details to add.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        /// <response code="200">Rarity added successfully.</response>
        /// <response code="400">Rarity already exists.</response>
        [HttpPost("addRarity")]
        public async Task<ActionResult<ApiResponse>> AddRarity(RarityAddRequest request)
        {
            _logger.LogInformation("Attempting to add new rarity: {@Request}", request);
            var response = await _mediator.Send(new CreateRarityCommand(request));

            if (response == null)
            {
                _logger.LogWarning("Failed to add rarity: Rarity already exists");
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Rarity already exists",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            _logger.LogInformation("Rarity added successfully: {@Response}", response);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Rarity added successfully",
                StatusCode = HttpStatusCode.OK,
                Result = response
            });
        }

        /// <summary>
        /// Updates an existing rarity in the system.
        /// </summary>
        /// <param name="request">The request containing updated rarity details.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        /// <response code="200">Rarity updated successfully.</response>
        /// <response code="404">Rarity not found.</response>
        [HttpPut("updateRarity")]
        public async Task<ActionResult<ApiResponse>> UpdateRarity(RarityUpdateRequest request)
        {
            _logger.LogInformation("Attempting to update rarity: {@Request}", request);
            var response = await _mediator.Send(new UpdateRarityCommand(request));
            if (response == null)
            {
                _logger.LogWarning("Failed to update rarity: Rarity not found");
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Rarity not found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Rarity updated successfully: {@Response}", response);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Rarity updated successfully",
                StatusCode = HttpStatusCode.OK,
                Result = response
            });
        }

        /// <summary>
        /// Deletes a rarity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the rarity to delete.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        /// <response code="200">Rarity deleted successfully.</response>
        /// <response code="404">Rarity not found.</response>
        [HttpDelete("deleteRarity/{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteRarity(Guid id)
        {
            _logger.LogInformation("Attempting to delete rarity with ID: {Id}", id);
            var response = await _mediator.Send(new DeleteRarityCommand(id));
            if (response == false)
            {
                _logger.LogWarning("Failed to delete rarity: Rarity not found for ID {Id}", id);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Rarity not found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Rarity deleted successfully for ID: {Id}", id);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Rarity deleted successfully",
                StatusCode = HttpStatusCode.OK,
                Result = response
            });
        }

        /// <summary>
        /// Retrieves a rarity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the rarity to retrieve.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the rarity details.</returns>
        /// <response code="200">Rarity retrieved successfully.</response>
        /// <response code="404">Rarity not found.</response>
        [HttpGet("getRarity/{id}")]
        public async Task<ActionResult<ApiResponse>> GetRarity(Guid id)
        {
            _logger.LogInformation("Attempting to retrieve rarity with ID: {Id}", id);
            var response = await _mediator.Send(new GetRarityByQuery(x => x.Id == id));
            if (response == null)
            {
                _logger.LogWarning("Failed to retrieve rarity: Rarity not found for ID {Id}", id);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Rarity not found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Rarity retrieved successfully for ID: {Id}", id);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Rarity retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = response
            });
        }

        /// <summary>
        /// Retrieves all rarities in the system.
        /// </summary>
        /// <returns>An <see cref="ApiResponse"/> containing a list of all rarities.</returns>
        /// <response code="200">Rarities retrieved successfully.</response>
        /// <response code="404">No rarities found.</response>
        [HttpGet("getAllRarities")]
        public async Task<ActionResult<ApiResponse>> GetAllRarities()
        {
            _logger.LogInformation("Attempting to retrieve all rarities");
            var response = await _mediator.Send(new GetAllRarityQuery(null));
            if (response == null)
            {
                _logger.LogWarning("Failed to retrieve rarities: No rarities found");
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "No rarities found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Rarities retrieved successfully");
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Rarities retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = response
            });
        }

        /// <summary>
        /// Retrieves rarities by name (case-insensitive partial match).
        /// </summary>
        /// <param name="name">The name or partial name of the rarity to search for.</param>
        /// <returns>An <see cref="ApiResponse"/> containing matching rarities.</returns>
        /// <response code="200">Rarity retrieved successfully.</response>
        /// <response code="404">Rarity not found.</response>
        [HttpGet("getRarityByName/{name}")]
        public async Task<ActionResult<ApiResponse>> GetRarityByName(string name)
        {
            _logger.LogInformation("Attempting to retrieve rarity by name: {Name}", name);
            var response = await _mediator.Send(new GetAllRarityQuery(x => x.Name.ToUpper().Contains(name.ToUpper())));
            if (response == null)
            {
                _logger.LogWarning("Failed to retrieve rarity: Rarity not found for name {Name}", name);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Rarity not found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Rarity retrieved successfully for name: {Name}", name);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Rarity retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = response
            });
        }
        /// <summary>
        /// Retrieves rarities by drop rate.
        /// </summary>
        /// <param name="dropRate">The drop rate of the rarity to retrieve.</param>
        /// <returns>An <see cref="ApiResponse"/> containing matching rarities.</returns>
        /// <response code="200">Rarity retrieved successfully.</response>
        /// <response code="404">Rarity not found.</response>
        [HttpGet("getRarityByDropRate/{dropRate}")]
        public async Task<ActionResult<ApiResponse>> GetRarityByDropRate(int dropRate)
        {
            _logger.LogInformation("Attempting to retrieve rarity by drop rate: {DropRate}", dropRate);
            var response = await _mediator.Send(new GetAllRarityQuery(x => x.DropRate == dropRate));
            if (response == null)
            {
                _logger.LogWarning("Failed to retrieve rarity: Rarity not found for drop rate {DropRate}", dropRate);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Rarity not found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Rarity retrieved successfully for drop rate: {DropRate}", dropRate);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Rarity retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = response
            });
        }

        /// <summary>
        /// Retrieves rarities by color code (case-insensitive partial match).
        /// </summary>
        /// <param name="colorCode">The color code or partial color code of the rarity to search for.</param>
        /// <returns>An <see cref="ApiResponse"/> containing matching rarities.</returns>
        /// <response code="200">Rarity retrieved successfully.</response>
        /// <response code="404">Rarity not found.</response>
        [HttpGet("getRarityByColorCode/{colorCode}")]
        public async Task<ActionResult<ApiResponse>> GetRarityByColorCode(string colorCode)
        {
            _logger.LogInformation("Attempting to retrieve rarity by color code: {ColorCode}", colorCode);
            var response = await _mediator.Send(new GetAllRarityQuery(x => x.ColorCode.ToUpper().Contains(colorCode.ToUpper())));
            if (response == null)
            {
                _logger.LogWarning("Failed to retrieve rarity: Rarity not found for color code {ColorCode}", colorCode);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Rarity not found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Rarity retrieved successfully for color code: {ColorCode}", colorCode);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Rarity retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = response
            });
        }
    }
}