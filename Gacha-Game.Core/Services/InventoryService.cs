using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Domain.RepositoryContract;
using Gacha_Game.Core.Dtos.InventoryDto;
using Gacha_Game.Core.Execptions;
using Gacha_Game.Core.ServiceContract;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Gacha_Game.Core.Services
{
    public class InventoryService(
        IUnitOfWork _unitOfWork,
        IUserContext _userContext,
        ILogger<InventoryService> _logger): IInventoryService
    {
        private async Task ExecuteWithTransactionAsync(Func<Task> action)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    _logger.LogInformation("Transaction started.");
                    await action();
                    await _unitOfWork.CommitTransactionAsync();
                    _logger.LogInformation("Transaction committed successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Transaction failed. Rolling back...");
                    await _unitOfWork.RollbackTransactionAsync();
                    throw;
                }
            }
        }
        public async Task<InventoryResponse> AddInventoryAsync(InventoryAddRequest? request)
        {
            if (request is null)
            {
                _logger.LogError("Request is null");    
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }
            var user = await _userContext.GetCurrentUserAsync();
            if (user is null)
            {
                _logger.LogError("User not found");
                throw new InvalidOperationException("User not found");
            }
            _logger.LogInformation("User found: {UserId}", user.Id);

            var character = await _unitOfWork.Repository<Character>()
                .GetByAsync(c => c.Id == request.CharacterID,includeProperties: "Rarity,ElementType");

            if (character is null)
            {
                _logger.LogError("Character not found: {CharacterId}", request.CharacterID);
                throw new InvalidOperationException("Character not found");
            }
            _logger.LogInformation("Character found: {CharacterId}", character.Id);
            var inventory = request.Adapt<Inventory>();
            inventory.UserID = user.Id;
            inventory.CharacterID = character.Id;
            inventory.ObtainedDate = DateTime.UtcNow;
            inventory.User = user;
            inventory.Character = character;

            _logger.LogInformation("Creating inventory for user: {UserId} and character: {CharacterId}", user.Id, character.Id);
            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<Inventory>().CreateAsync(inventory);
            });
            _logger.LogInformation("Inventory created successfully for user: {UserId} and character: {CharacterId}", user.Id, character.Id);
            return inventory.Adapt<InventoryResponse>();

        }

        public async Task<IEnumerable<InventoryResponse>> GetInventoriesAsync(Expression<Func<Inventory,bool>>? expression = null)
        {
            _logger.LogInformation("Fetching inventories with expression: {Expression}", expression);
            var inventories = await _unitOfWork.Repository<Inventory>()
                .GetAllAsync(expression, includeProperties: "User,Character,Character.ElementType,Character.Rarity");

            if (inventories is null || !inventories.Any())
            {
                _logger.LogWarning("No inventories found with the provided expression: {Expression}", expression);
                return Enumerable.Empty<InventoryResponse>();
            }
            _logger.LogInformation("Inventories fetched successfully: {Count}", inventories.Count());
            return inventories.Adapt<IEnumerable<InventoryResponse>>();
        }

        public async Task<InventoryResponse> GetInventoryByAsync(Expression<Func<Inventory, bool>> expression)
        {
            _logger.LogInformation("Fetching inventory with expression: {Expression}", expression);
            var inventory = await _unitOfWork.Repository<Inventory>()
                .GetByAsync(expression, includeProperties: "User,Character,Character.ElementType,Character.Rarity");

            if (inventory is null)
            {
                _logger.LogWarning("Inventory not found with the provided expression: {Expression}", expression);
                throw new NotFoundException("Inventory not found");
            }
            _logger.LogInformation("Inventory fetched successfully: {InventoryId}", inventory.Id);
            return inventory.Adapt<InventoryResponse>();
        }
    }
}
