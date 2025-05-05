using Gacha_Game.Core.CQRS.Command.InventoryCommand;
using Gacha_Game.Core.CQRS.Queries.InventoryQueries;
using Gacha_Game.Core.Dtos;
using Gacha_Game.Core.Dtos.InventoryDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gacha_Game.Controllers
{
    /// <summary>
    /// Controller for managing inventory-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InventoryController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR mediator for handling commands and queries.</param>
        /// <param name="logger">The logger for logging controller actions.</param>
        public InventoryController(IMediator mediator, ILogger<InventoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new inventory item.
        /// </summary>
        /// <param name="request">The request containing details for the new inventory item.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        /// <response code="200">Returns the created inventory item details if successful.</response>
        /// <response code="400">Returns an error message if the inventory could not be added.</response>
        [HttpPost("addInventory")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> AddInventory(InventoryAddRequest request)
        {
            var response = await _mediator.Send(new CreateInventoryCommand(request));
            if (response == null)
            {
                _logger.LogError("Failed to add inventory");
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to add inventory",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            _logger.LogInformation("Inventory added successfully");
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Inventory added successfully",
                Result = response
            });
        }

        /// <summary>
        /// Retrieves an inventory item by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the inventory item.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the inventory item details.</returns>
        /// <response code="200">Returns the inventory item if found.</response>
        /// <response code="400">Returns an error message if the inventory item could not be retrieved.</response>
        /// <response code="401">Returns an error message if the user is not authorized.</response>
        [HttpGet("getInventory/{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> GetInventory(Guid id)
        {
            var response = await _mediator.Send(new GetInventoryByQuey(x => x.Id == id));
            if (response == null)
            {
                _logger.LogError("Failed to get inventory");
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get inventory",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            _logger.LogInformation("Inventory retrieved successfully");
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Inventory retrieved successfully",
                Result = response
            });
        }

        /// <summary>
        /// Retrieves all inventory items.
        /// </summary>
        /// <returns>An <see cref="ApiResponse"/> containing a list of all inventory items.</returns>
        /// <response code="200">Returns the list of inventory items if successful.</response>
        /// <response code="400">Returns an error message if the inventory items could not be retrieved.</response>
        [HttpGet("getAllInventory")]
        public async Task<ActionResult<ApiResponse>> GetAllInventory()
        {
            var response = await _mediator.Send(new GetAllInventoriesQuery(null));
            if (response == null)
            {
                _logger.LogError("Failed to get all inventory");
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get all inventory",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            _logger.LogInformation("All inventory retrieved successfully");
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "All inventory retrieved successfully",
                Result = response
            });
        }

        /// <summary>
        /// Retrieves inventory items for a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the user's inventory items.</returns>
        /// <response code="200">Returns the user's inventory items if found.</response>
        /// <response code="400">Returns an error message if the inventory items could not be retrieved.</response>
        /// <response code="401">Returns an error message if the user is not authorized.</response>
        [HttpGet("getInventoryByUserId/{userId}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> GetInventoryByUserId(Guid userId)
        {
            var response = await _mediator.Send(new GetAllInventoriesQuery(x => x.UserID == userId));
            if (response == null)
            {
                _logger.LogError("Failed to get inventory by user id");
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get inventory by user id",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            _logger.LogInformation("Inventory by user id retrieved successfully");
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Inventory by user id retrieved successfully",
                Result = response
            });
        }
    }
}