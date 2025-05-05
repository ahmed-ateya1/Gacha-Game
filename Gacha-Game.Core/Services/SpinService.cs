using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Domain.RepositoryContract;
using Gacha_Game.Core.Dtos.CharacterDto;
using Gacha_Game.Core.Dtos.SpinDto;
using Gacha_Game.Core.Execptions;
using Gacha_Game.Core.Helper;
using Gacha_Game.Core.ServiceContract;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Gacha_Game.Core.Services
{
    public class SpinService(IUnitOfWork _unitOfWork, 
        ILogger<SpinService> _logger,
        IUserContext _userContext)
        : ISpinService
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
        public async Task<GachaSpinResponse> SpinAsync(SpinAddRequest request)
        {
            GachaSpinResponse response = null!;

            await ExecuteWithTransactionAsync(async () =>
            {
                var banner = await _unitOfWork.Repository<GachaBanners>()
                    .GetByAsync(b => b.Id == request.BannerID && b.StartDate <= DateTime.UtcNow && b.EndDate >= DateTime.UtcNow, includeProperties: "BannerCharacters.Character.Rarity,BannerCharacters.Character.ElementType,GachaPulls");
                _logger.LogInformation("Banner: {Banner}", banner);

                if (banner is null)
                    throw new NotFoundException("Banner not found or inactive.");

                var user = await _userContext.GetCurrentUserAsync();
                _logger.LogInformation("User: {User}", user);

                if (user is null)
                    throw new NotFoundException("User not found.");

                int totalCost = banner.CostPerPull * (int)request.PullType;

                if (user.Currancy < totalCost)
                {
                    _logger.LogInformation("User: {User} has not enough gems", user);
                    throw new Exception("Not enough gems.");
                }

                var bannerPool = BuildWeightedPoolHelper.BuildWeightedPool(banner).ToList();
                if (bannerPool.Count == 0)
                    throw new Exception("Banner pool is empty.");

                var rng = new Random();
                var pulledCharacters = new List<Character>();

                int totalPullsSoFar = banner.GachaPulls
                    .Where(p => p.UserId == user.Id)
                    .Sum(p => p.PullCount);

                for (int i = 1; i <= (int)request.PullType; i++)
                {
                    int currentPullNumber = totalPullsSoFar + i;
                    Character selected;

                    if (banner.PullsForGuaranteedRarity > 0 &&
                        currentPullNumber % banner.PullsForGuaranteedRarity == 0)
                    {
                        selected = banner.BannerCharacters
                            .Select(bc => bc.Character)
                            .OrderBy(c => c.Rarity!.DropRate)
                            .First();
                    }
                    else
                    {
                        selected = bannerPool[rng.Next(bannerPool.Count)];
                    }

                    pulledCharacters.Add(selected);
                }

                var gachaPull = new GachaPulls
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    BannerId = request.BannerID,
                    PullType = request.PullType,
                    PullCount = (int)request.PullType,
                    PullDate = DateTime.UtcNow
                };
                await _unitOfWork.Repository<GachaPulls>().CreateAsync(gachaPull);

                var characterResponses = new List<CharacterResponse>();

                foreach (var character in pulledCharacters)
                {
                    var inventory = new Inventory
                    {
                        Id = Guid.NewGuid(),
                        UserID = user.Id,
                        CharacterID = character.Id,
                        ObtainedDate = DateTime.UtcNow
                    };
                    await _unitOfWork.Repository<Inventory>().CreateAsync(inventory);

                    var pullResult = new PullResult
                    {
                        Id = Guid.NewGuid(),
                        PullId = gachaPull.Id,
                        InventoryId = inventory.Id
                    };
                    await _unitOfWork.Repository<PullResult>().CreateAsync(pullResult);

                    characterResponses.Add(character.Adapt<CharacterResponse>());
                }

                user.Currancy -= totalCost;
                await _unitOfWork.CompleteAsync();

                response = new GachaSpinResponse
                {
                    BannerId = request.BannerID,
                    PullType = request.PullType,
                    Characters = characterResponses
                };
            });

            return response;
        }

    }
}
