using Gacha_Game.Core.CQRS.Command.ElementTypeCommand;
using Gacha_Game.Core.CQRS.Queries.ElementTypeQueries;
using Gacha_Game.Core.Dtos;
using Gacha_Game.Core.Dtos.ElementTypeDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gacha_Game.Controllers
{
    /// <summary>
    /// API controller for managing ElementType entities in the Gacha Game application.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ElementTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ElementTypeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementTypeController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR mediator for handling CQRS commands and queries.</param>
        /// <param name="logger">The logger for capturing controller events and errors.</param>
        public ElementTypeController(IMediator mediator, ILogger<ElementTypeController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Adds a new ElementType to the system.
        /// </summary>
        /// <param name="request">The request containing ElementType details (e.g., Name, Icon).</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the operation result.</returns>
        /// <response code="200">Returns the created ElementType details.</response>
        /// <response code="400">If the request is invalid or the operation fails.</response>
        [HttpPost("addElement")]
        public async Task<ActionResult<ApiResponse>> AddElement([FromForm]ElementTypeAddRequest request)
        {
            _logger.LogInformation("Received request to add ElementType with Name: {Name}", request?.Name);

            var result = await _mediator.Send(new CreateElementTypeCommand(request));

            if (result == null)
            {
                _logger.LogWarning("Failed to add ElementType with Name: {Name}", request?.Name);
                return BadRequest(new ApiResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    Message = "Failed to add element type"
                });
            }

            _logger.LogInformation("ElementType added successfully. Id: {Id}, Name: {Name}", result.Id, result.Name);
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Element type added successfully",
                Result = result
            });
        }

        /// <summary>
        /// Updates an existing ElementType.
        /// </summary>
        /// <param name="request">The request containing updated ElementType details (e.g., Id, Name, Icon).</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the operation result.</returns>
        /// <response code="200">Returns the updated ElementType details.</response>
        /// <response code="404">If the ElementType with the specified Id is not found.</response>
        [HttpPut("updateElement")]
        public async Task<ActionResult<ApiResponse>> UpdateElementType([FromForm]ElementTypeUpdateRequest request)
        {
            _logger.LogInformation("Received request to update ElementType with Id: {Id}, Name: {Name}", request?.Id, request?.Name);

            var result = await _mediator.Send(new UpdateElementTypeCommand(request));
            if (result == null)
            {
                _logger.LogWarning("ElementType not found for update. Id: {Id}", request?.Id);
                return NotFound(new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    Message = "Element type not found"
                });
            }

            _logger.LogInformation("ElementType updated successfully. Id: {Id}, Name: {Name}", result.Id, result.Name);
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Element type updated successfully",
                Result = result
            });
        }

        /// <summary>
        /// Deletes an ElementType by its Id.
        /// </summary>
        /// <param name="id">The unique identifier of the ElementType to delete.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the operation result.</returns>
        /// <response code="200">Confirms the ElementType was deleted successfully.</response>
        /// <response code="404">If the ElementType with the specified Id is not found.</response>
        [HttpDelete("deleteElementType/{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteElementType(Guid id)
        {
            _logger.LogInformation("Received request to delete ElementType with Id: {Id}", id);

            var result = await _mediator.Send(new DeleteElementTypeCommand(id));
            if (result == false)
            {
                _logger.LogWarning("ElementType not found for deletion. Id: {Id}", id);
                return NotFound(new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    Message = "Element type not found"
                });
            }

            _logger.LogInformation("ElementType deleted successfully. Id: {Id}", id);
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Element type deleted successfully",
                Result = result
            });
        }

        /// <summary>
        /// Retrieves all ElementTypes.
        /// </summary>
        /// <returns>An <see cref="ApiResponse"/> containing a list of ElementTypes.</returns>
        /// <response code="200">Returns the list of ElementTypes.</response>
        /// <response code="404">If no ElementTypes are found.</response>
        [HttpGet("getAllElementTypes")]
        public async Task<ActionResult<ApiResponse>> GetAllElementTypes()
        {
            _logger.LogInformation("Received request to retrieve all ElementTypes");

            var result = await _mediator.Send(new GetAllElementTypesQuery(null));
            if (result == null || !result.Any())
            {
                _logger.LogWarning("No ElementTypes found");
                return NotFound(new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    Message = "No element types found"
                });
            }

            _logger.LogInformation("Retrieved {Count} ElementTypes successfully", result.Count());
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Element types retrieved successfully",
                Result = result
            });
        }

        /// <summary>
        /// Retrieves an ElementType by its Id.
        /// </summary>
        /// <param name="id">The unique identifier of the ElementType to retrieve.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the ElementType details.</returns>
        /// <response code="200">Returns the ElementType details.</response>
        /// <response code="404">If the ElementType with the specified Id is not found.</response>
        [HttpGet("getElementTypeById/{id}")]
        public async Task<ActionResult<ApiResponse>> GetElementTypeById(Guid id)
        {
            _logger.LogInformation("Received request to retrieve ElementType by Id: {Id}", id);

            var result = await _mediator.Send(new GetElementTypeByQuery(x => x.Id == id));
            if (result == null)
            {
                _logger.LogWarning("ElementType not found. Id: {Id}", id);
                return NotFound(new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    Message = "Element type not found"
                });
            }

            _logger.LogInformation("ElementType retrieved successfully. Id: {Id}, Name: {Name}", result.Id, result.Name);
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Element type retrieved successfully",
                Result = result
            });
        }

        /// <summary>
        /// Retrieves an ElementType by its name (case-insensitive partial match).
        /// </summary>
        /// <param name="name">The name or partial name of the ElementType to search for.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the ElementType details.</returns>
        /// <response code="200">Returns the ElementType details.</response>
        /// <response code="404">If no ElementType matches the specified name.</response>
        [HttpGet("getElementTypeByName/{name}")]
        public async Task<ActionResult<ApiResponse>> GetElementTypeByName(string name)
        {
            _logger.LogInformation("Received request to retrieve ElementType by Name: {Name}", name);

            var result = await _mediator.Send(new GetElementTypeByQuery(x => x.Name.ToUpper().Contains(name.ToUpper())));
            if (result == null)
            {
                _logger.LogWarning("ElementType not found for Name: {Name}", name);
                return NotFound(new ApiResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    Message = "Element type not found"
                });
            }

            _logger.LogInformation("ElementType retrieved successfully. Id: {Id}, Name: {Name}", result.Id, result.Name);
            return Ok(new ApiResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Element type retrieved successfully",
                Result = result
            });
        }
    }
}