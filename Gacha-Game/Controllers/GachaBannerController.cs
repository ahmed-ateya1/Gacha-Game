using Gacha_Game.Core.CQRS.Command.GachaBannerCommand;
using Gacha_Game.Core.CQRS.Queries.GachaBannerQueries;
using Gacha_Game.Core.Dtos;
using Gacha_Game.Core.Dtos.GachaBannerDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Gacha_Game.Controllers
{
    /// <summary>
    /// Handles CRUD operations and queries for Gacha Banners.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GachaBannerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GachaBannerController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GachaBannerController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling CQRS commands and queries.</param>
        /// <param name="logger">The logger for logging controller actions.</param>
        public GachaBannerController(IMediator mediator, ILogger<GachaBannerController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Adds a new Gacha Banner.
        /// </summary>
        /// <param name="request">The request containing Gacha Banner details.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        [HttpPost("addGachaBanner")]
        public async Task<ActionResult<ApiResponse>> AddGachaBanner([FromForm] GachaBannerAddRequest request)
        {
            _logger.LogInformation("Adding new Gacha Banner: {Name}", request.Name);
            var result = await _mediator.Send(new CreateGachaBannerCommand(request));
            if (result == null)
            {
                _logger.LogWarning("Failed to create Gacha Banner: {Name}", request.Name);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to create Gacha Banner",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            _logger.LogInformation("Gacha Banner created successfully: {Name}", request.Name);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Gacha Banner created successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Updates an existing Gacha Banner.
        /// </summary>
        /// <param name="request">The request containing updated Gacha Banner details.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        [HttpPut("updateGachaBanner")]
        public async Task<ActionResult<ApiResponse>> UpdateGachaBanner([FromForm] GachaBannerUpdateRequest request)
        {
            _logger.LogInformation("Updating Gacha Banner with ID: {Id}", request.Id);
            var result = await _mediator.Send(new UpdateGachaBannerCommand(request));
            if (result == null)
            {
                _logger.LogWarning("Failed to update Gacha Banner with ID: {Id}", request.Id);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to update Gacha Banner",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            _logger.LogInformation("Gacha Banner updated successfully: {Id}", request.Id);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Gacha Banner updated successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Deletes a Gacha Banner by ID.
        /// </summary>
        /// <param name="id">The ID of the Gacha Banner to delete.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        [HttpDelete("deleteGachaBanner/{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteGachaBanner(Guid id)
        {
            _logger.LogInformation("Deleting Gacha Banner with ID: {Id}", id);
            var result = await _mediator.Send(new DeleteGachaBannerCommand(id));
            if (!result)
            {
                _logger.LogWarning("Failed to delete Gacha Banner with ID: {Id}", id);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to delete Gacha Banner",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            _logger.LogInformation("Gacha Banner deleted successfully: {Id}", id);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Gacha Banner deleted successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves a Gacha Banner by ID.
        /// </summary>
        /// <param name="id">The ID of the Gacha Banner to retrieve.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the Gacha Banner details.</returns>
        [HttpGet("getGachaBanner/{id}")]
        public async Task<ActionResult<ApiResponse>> GetGachaBanner(Guid id)
        {
            _logger.LogInformation("Retrieving Gacha Banner with ID: {Id}", id);
            var result = await _mediator.Send(new GetByGachaBannerQuery(x => x.Id == id));
            if (result == null)
            {
                _logger.LogWarning("Gacha Banner not found with ID: {Id}", id);
                return NotFound(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Gacha Banner not found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Gacha Banner retrieved successfully: {Id}", id);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Gacha Banner retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves all Gacha Banners.
        /// </summary>
        /// <returns>An <see cref="ApiResponse"/> containing a list of all Gacha Banners.</returns>
        [HttpGet("getAllGachaBanners")]
        public async Task<ActionResult<ApiResponse>> GetAllGachaBanners()
        {
            _logger.LogInformation("Retrieving all Gacha Banners");
            var result = await _mediator.Send(new GetAllGachaBannerQuery(null));
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No Gacha Banners found");
                return NotFound(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "No Gacha Banners found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Gacha Banners retrieved successfully. Count: {Count}", result.Count());
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Gacha Banners retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves all active Gacha Banners.
        /// </summary>
        /// <returns>An <see cref="ApiResponse"/> containing a list of active Gacha Banners.</returns>
        [HttpGet("getActiveGachaBanners")]
        public async Task<ActionResult<ApiResponse>> GetActiveGachaBanners()
        {
            _logger.LogInformation("Retrieving active Gacha Banners");
            var result = await _mediator.Send(new GetAllGachaBannerQuery(x => x.StartDate <= DateTime.UtcNow && x.EndDate >= DateTime.UtcNow));
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No active Gacha Banners found");
                return NotFound(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "No active Gacha Banners found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Active Gacha Banners retrieved successfully. Count: {Count}", result.Count());
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Active Gacha Banners retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves all inactive Gacha Banners.
        /// </summary>
        /// <returns>An <see cref="ApiResponse"/> containing a list of inactive Gacha Banners.</returns>
        [HttpGet("getInActiveGachaBanners")]
        public async Task<ActionResult<ApiResponse>> GetInActiveGachaBanners()
        {
            _logger.LogInformation("Retrieving inactive Gacha Banners");
            var result = await _mediator.Send(new GetAllGachaBannerQuery(x => x.EndDate < DateTime.UtcNow || x.StartDate > DateTime.UtcNow));
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No inactive Gacha Banners found");
                return NotFound(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "No inactive Gacha Banners found",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Inactive Gacha Banners retrieved successfully. Count: {Count}", result.Count());
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Inactive Gacha Banners retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves Gacha Banners within a specified date range.
        /// </summary>
        /// <param name="startDate">The start date for filtering Gacha Banners.</param>
        /// <param name="endDate">The end date for filtering Gacha Banners.</param>
        /// <returns>An <see cref="ApiResponse"/> containing a list of Gacha Banners in the date range.</returns>
        [HttpGet("getGachaBannersByDate")]
        public async Task<ActionResult<ApiResponse>> GetGachaBannersByDate(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation("Retrieving Gacha Banners between {StartDate} and {EndDate}", startDate, endDate);
            var result = await _mediator.Send(new GetAllGachaBannerQuery(x => x.StartDate >= startDate && x.EndDate <= endDate));
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No Gacha Banners found between {StartDate} and {EndDate}", startDate, endDate);
                return NotFound(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "No Gacha Banners found in the specified date range",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Gacha Banners retrieved successfully between {StartDate} and {EndDate}. Count: {Count}", startDate, endDate, result.Count());
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Gacha Banners retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves Gacha Banners by name (case-insensitive partial match).
        /// </summary>
        /// <param name="name">The name or partial name to search for.</param>
        /// <returns>An <see cref="ApiResponse"/> containing a list of matching Gacha Banners.</returns>
        [HttpGet("getGachaBannersByName")]
        public async Task<ActionResult<ApiResponse>> GetGachaBannersByName(string name)
        {
            _logger.LogInformation("Retrieving Gacha Banners with name: {Name}", name);
            var result = await _mediator.Send(new GetAllGachaBannerQuery(x => x.Name.ToUpper().Contains(name.ToUpper())));
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No Gacha Banners found with name: {Name}", name);
                return NotFound(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "No Gacha Banners found with the specified name",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Gacha Banners retrieved successfully with name: {Name}. Count: {Count}", name, result.Count());
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Gacha Banners retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }
    }
}