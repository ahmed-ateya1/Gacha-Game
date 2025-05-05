using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Domain.RepositoryContract;
using Gacha_Game.Core.Dtos.RarityDto;
using Gacha_Game.Core.Execptions;
using Gacha_Game.Core.ServiceContract;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Gacha_Game.Core.Services
{
    public class RarityService (IUnitOfWork _unitOfWork , ILogger<RarityService> _logger)
        : IRarityService
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

        public async Task<RarityResponse> AddRarityAsync(RarityAddRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var rarity = request.Adapt<Rarity>();
            rarity.Id = Guid.NewGuid();
            
            _logger.LogInformation("Adding new rarity with ID: {Id}", rarity.Id);

            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<Rarity>().CreateAsync(rarity);
            });

            _logger.LogInformation("Rarity added successfully.");
            return rarity.Adapt<RarityResponse>();
        }

        public async Task<bool> DeleteRarityAsync(Guid id)
        {
            var rarity = await _unitOfWork.Repository<Rarity>()
                .GetByAsync(x => x.Id == id);

            if (rarity == null)
            {
                _logger.LogWarning("Rarity with ID: {Id} not found.", id);
                return false;
            }
            _logger.LogInformation("Deleting rarity with ID: {Id}", id);

            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<Rarity>().DeleteAsync(rarity);
            });

            _logger.LogInformation("Rarity deleted successfully.");

            return true;
        }

        public async Task<IEnumerable<RarityResponse>> GetAllRaritiesAsync(Expression<Func<Rarity, bool>>? expression = null)
        {
            var rarities = await _unitOfWork.Repository<Rarity>()
                .GetAllAsync(expression);

            if(rarities == null || !rarities.Any())
            {
                _logger.LogWarning("No rarities found.");
                return Enumerable.Empty<RarityResponse>();
            }

            _logger.LogInformation("Retrieved {Count} rarities.", rarities.Count());
            return rarities.Adapt<IEnumerable<RarityResponse>>();
        }

        public async Task<RarityResponse?> GetRarityByAsync(Expression<Func<Rarity, bool>> expression, bool isTracked = false)
        {
            var rarity = await _unitOfWork.Repository<Rarity>()
                .GetByAsync(expression, isTracked);

            if(rarity == null)
            {
                _logger.LogWarning("Rarity not found.");
                throw new NotFoundException("Rarity not found.");
            }
            _logger.LogInformation("Rarity found with ID: {Id}", rarity.Id);

            return rarity.Adapt<RarityResponse>();
        }

        public async Task<RarityResponse> UpdateRarityAsync(RarityUpdateRequest request)
        {
            var rarity = await _unitOfWork.Repository<Rarity>()
                .GetByAsync(x => x.Id == request.Id);

            if (rarity == null)
            {
                _logger.LogWarning("Rarity with ID: {Id} not found.", request.Id);
                throw new NotFoundException("Rarity not found.");
            }
            _logger.LogInformation("Updating rarity with ID: {Id}", request.Id);

            request.Adapt(rarity);

            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<Rarity>().UpdateAsync(rarity);
            });

            _logger.LogInformation("Rarity updated successfully.");

            return rarity.Adapt<RarityResponse>();
        }
    }
}
