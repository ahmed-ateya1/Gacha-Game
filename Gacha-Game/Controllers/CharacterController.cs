using Gacha_Game.Core.CQRS.Command.CharacterCommand;
using Gacha_Game.Core.CQRS.Queries.CharacterQueries;
using Gacha_Game.Core.Dtos;
using Gacha_Game.Core.Dtos.CharacterDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Gacha_Game.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IMediator _sender;
        private readonly ILogger<CharacterController> _logger;

        public CharacterController(IMediator sender, ILogger<CharacterController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new character to the system.
        /// </summary>
        /// <param name="command">The character creation details.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        /// <response code="200">Character added successfully.</response>
        /// <response code="400">Failed to add character due to invalid data or processing error.</response>
        [HttpPost("addCharacter")]
        public async Task<ActionResult<ApiResponse>> AddCharacter([FromForm] CharacterAddRequest command)
        {
            _logger.LogInformation("Attempting to add character with name: {Name}", command.Name);
            var result = await _sender.Send(new CreateCharacterCommand(command));
            if (result == null)
            {
                _logger.LogError("Failed to add character with name: {Name}", command.Name);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to add character",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            _logger.LogInformation("Character with name: {Name} added successfully", command.Name);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character added successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Updates an existing character.
        /// </summary>
        /// <param name="command">The character update details.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        /// <response code="200">Character updated successfully.</response>
        /// <response code="400">Failed to update character due to invalid data or processing error.</response>
        [HttpPut("updateCharacter")]
        public async Task<ActionResult<ApiResponse>> UpdateCharacter([FromForm] CharacterUpdateRequest command)
        {
            _logger.LogInformation("Attempting to update character with ID: {Id}", command.Id);
            var result = await _sender.Send(new UpdateCharacterCommand(command));
            if (result == null)
            {
                _logger.LogError("Failed to update character with ID: {Id}", command.Id);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to update character",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            _logger.LogInformation("Character with ID: {Id} updated successfully", command.Id);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character updated successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Deletes a character by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the character to delete.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        /// <response code="200">Character deleted successfully.</response>
        /// <response code="404">Character not found or failed to delete.</response>
        [HttpDelete("deleteCharacter/{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteCharacter(Guid id)
        {
            _logger.LogInformation("Attempting to delete character with ID: {Id}", id);
            var result = await _sender.Send(new DeleteCharacterCommand(id));
            if (result == false)
            {
                _logger.LogError("Failed to delete character with ID: {Id}", id);
                return NotFound(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to delete character",
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            _logger.LogInformation("Character with ID: {Id} deleted successfully", id);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character deleted successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves all characters.
        /// </summary>
        /// <returns>An <see cref="ApiResponse"/> containing the list of characters.</returns>
        /// <response code="200">Characters retrieved successfully.</response>
        /// <response code="400">Failed to retrieve characters.</response>
        [HttpGet("getAllCharacters")]
        public async Task<ActionResult<ApiResponse>> GetAllCharacters()
        {
            _logger.LogInformation("Attempting to retrieve all characters");
            var result = await _sender.Send(new GetAllCharacterQury(null));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve all characters");
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get all characters",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            _logger.LogInformation("Successfully retrieved {Count} characters", result.Count());
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Characters retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves a character by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the character.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the character details.</returns>
        /// <response code="200">Character retrieved successfully.</response>
        /// <response code="400">Failed to retrieve character.</response>
        [HttpGet("getCharacter/{id}")]
        public async Task<ActionResult<ApiResponse>> GetCharacter(Guid id)
        {
            _logger.LogInformation("Attempting to retrieve character with ID: {Id}", id);
            var result = await _sender.Send(new GetCharacterByQuery(x => x.Id == id));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve character with ID: {Id}", id);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get character",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            _logger.LogInformation("Character with ID: {Id} retrieved successfully", id);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves characters by name (case-insensitive partial match).
        /// </summary>
        /// <param name="name">The name or partial name of the character.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the matching characters.</returns>
        /// <response code="200">Characters retrieved successfully.</response>
        /// <response code="400">Failed to retrieve characters.</response>
        [HttpGet("getCharacterByName/{name}")]
        public async Task<ActionResult<ApiResponse>> GetCharacterByName(string name)
        {
            _logger.LogInformation("Attempting to retrieve characters with name: {Name}", name);
            var result = await _sender.Send(new GetCharacterByQuery(x => x.Name.ToUpper().Contains(name.ToUpper())));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve characters with name: {Name}", name);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get character",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves characters by rarity ID.
        /// </summary>
        /// <param name="rarityId">The unique identifier of the rarity type.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the matching characters.</returns>
        /// <response code="200">Characters retrieved successfully.</response>
        /// <response code="400">Failed to retrieve characters.</response>
        [HttpGet("getCharacterByRarity/{rarityId}")]
        public async Task<ActionResult<ApiResponse>> GetCharacterByRarity(Guid rarityId)
        {
            _logger.LogInformation("Attempting to retrieve characters with rarity ID: {RarityId}", rarityId);
            var result = await _sender.Send(new GetCharacterByQuery(x => x.RarityId == rarityId));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve characters with rarity ID: {RarityId}", rarityId);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get character",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves characters by element type ID.
        /// </summary>
        /// <param name="elementId">The unique identifier of the element type.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the matching characters.</returns>
        /// <response code="200">Characters retrieved successfully.</response>
        /// <response code="400">Failed to retrieve characters.</response>
        [HttpGet("getCharacterByElement/{elementId}")]
        public async Task<ActionResult<ApiResponse>> GetCharacterByElement(Guid elementId)
        {
            _logger.LogInformation("Attempting to retrieve characters with element ID: {ElementId}", elementId);
            var result = await _sender.Send(new GetCharacterByQuery(x => x.ElementTypeId == elementId));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve characters with element ID: {ElementId}", elementId);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get character",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves characters by base attack value.
        /// </summary>
        /// <param name="attackBase">The base attack value of the character.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the matching characters.</returns>
        /// <response code="200">Characters retrieved successfully.</response>
        /// <response code="400">Failed to retrieve characters.</response>
        [HttpGet("getCharacterByAttack/{attackBase}")]
        public async Task<ActionResult<ApiResponse>> GetCharacterByAttack(int attackBase)
        {
            _logger.LogInformation("Attempting to retrieve characters with base attack: {AttackBase}", attackBase);
            var result = await _sender.Send(new GetCharacterByQuery(x => x.AttackBase == attackBase));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve characters with base attack: {AttackBase}", attackBase);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get character",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves characters by base defense value.
        /// </summary>
        /// <param name="defenseBase">The base defense value of the character.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the matching characters.</returns>
        /// <response code="200">Characters retrieved successfully.</response>
        /// <response code="400">Failed to retrieve characters.</response>
        [HttpGet("getCharacterByDefense/{defenseBase}")]
        public async Task<ActionResult<ApiResponse>> GetCharacterByDefense(int defenseBase)
        {
            _logger.LogInformation("Attempting to retrieve characters with base defense: {DefenseBase}", defenseBase);
            var result = await _sender.Send(new GetCharacterByQuery(x => x.DefenseBase == defenseBase));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve characters with base defense: {DefenseBase}", defenseBase);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get character",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves characters by base health value.
        /// </summary>
        /// <param name="healthBase">The base health value of the character.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the matching characters.</returns>
        /// <response code="200">Characters retrieved successfully.</response>
        /// <response code="400">Failed to retrieve characters.</response>
        [HttpGet("getCharacterByHealth/{healthBase}")]
        public async Task<ActionResult<ApiResponse>> GetCharacterByHealth(int healthBase)
        {
            _logger.LogInformation("Attempting to retrieve characters with base health: {HealthBase}", healthBase);
            var result = await _sender.Send(new GetCharacterByQuery(x => x.HealthBase == healthBase));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve characters with base health: {HealthBase}", healthBase);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get character",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        /// <summary>
        /// Retrieves characters by base attack and defense values.
        /// </summary>
        /// <param name="attackBase">The base attack value of the character.</param>
        /// <param name="defenseBase">The base defense value of the character.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the matching characters.</returns>
        /// <response code="200">Characters retrieved successfully.</response>
        /// <response code="400">Failed to retrieve characters.</response>
        [HttpGet("getCharacterByAttackAndDefense/{attackBase}/{defenseBase}")]
        public async Task<ActionResult<ApiResponse>> GetCharacterByAttackAndDefense(int attackBase, int defenseBase)
        {
            _logger.LogInformation("Attempting to retrieve characters with base attack: {AttackBase} and base defense: {DefenseBase}", attackBase, defenseBase);
            var result = await _sender.Send(new GetCharacterByQuery(x => x.AttackBase == attackBase && x.DefenseBase == defenseBase));
            if (result == null)
            {
                _logger.LogError("Failed to retrieve characters with base attack: {AttackBase} and base defense: {DefenseBase}", attackBase, defenseBase);
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to get character",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Character retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }
    }
}