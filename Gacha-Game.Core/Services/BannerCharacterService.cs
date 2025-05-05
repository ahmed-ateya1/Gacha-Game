using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Domain.RepositoryContract;
using Gacha_Game.Core.Dtos.BannerCharacterDto;
using Gacha_Game.Core.Dtos.CharacterDto;
using Gacha_Game.Core.Execptions;
using Gacha_Game.Core.ServiceContract;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Gacha_Game.Core.Services
{
    public class BannerCharacterService(
         IUnitOfWork _unitOfWork,
         ILogger<BannerCharacterService> _logger)
        : IBannerCharacterService
    {
        private async Task ChackCharacterAsync(IEnumerable<Guid> characterIds)
        {
            foreach (var characterID in characterIds)
            {
               if(await _unitOfWork.Repository<Character>().AnyAsync(x => x.Id == characterID) == false)
               {
                    throw new NotFoundException($"Character with ID {characterID} not found.");
               }
            }
        }
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

        public async Task<BannerCharaterResponse> AddBannerCharacterAsync(BannerCharacterAddRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var banner = await _unitOfWork.Repository<GachaBanners>()
                .GetByAsync(x => x.Id == request.BannerId);

            if (banner == null)
                throw new NotFoundException("Banner not found.");

            await ChackCharacterAsync(request.CharacterIds);

            List<CharacterResponse> characterResponses = new();

            await ExecuteWithTransactionAsync(async () =>
            {
                foreach (var characterId in request.CharacterIds)
                {
                    bool alreadyExists = await _unitOfWork.Repository<BannerCharacter>().AnyAsync(
                        x => x.BannerId == request.BannerId && x.CharacterId == characterId);

                    if (alreadyExists)
                        continue;

                    var character = await _unitOfWork.Repository<Character>()
                        .GetByAsync(x => x.Id == characterId,
                        includeProperties: "Rarity,ElementType");

                    if (character == null)
                        continue;

                    await _unitOfWork.Repository<BannerCharacter>().CreateAsync(new BannerCharacter
                    {
                        BannerId = request.BannerId,
                        CharacterId = characterId
                    });

                    var mapped = character.Adapt<CharacterResponse>();
                    characterResponses.Add(mapped);
                }
            });

            return new BannerCharaterResponse
            {
                BannerId = request.BannerId,
                CharacterResponses = characterResponses
            };
        }


        public async Task<bool> DeleteBannerCharacterByIdAsync(Guid id)
        {
            var bannerCharacter = await _unitOfWork.Repository<BannerCharacter>()
                .GetByAsync(x => x.Id == id);
            if (bannerCharacter == null)
                throw new NotFoundException("BannerCharacter not found.");

            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<BannerCharacter>().DeleteAsync(bannerCharacter);
            });
            return true;
        }

        public async Task<IEnumerable<BannerCharaterResponse>> GetAllBannerCharacterAsync(Expression<Func<BannerCharacter, bool>>? expression)
        {
            var bannerCharacters = await _unitOfWork.Repository<BannerCharacter>()
                .GetAllAsync(expression, includeProperties: "Banner,Character,Character.Rarity,Character.ElementType");

            if (bannerCharacters == null || !bannerCharacters.Any())
                return Enumerable.Empty<BannerCharaterResponse>();

            return bannerCharacters.Adapt<IEnumerable<BannerCharaterResponse>>();
        }

        public async Task<BannerCharaterResponse> GetBannerCharaterByAsync(Expression<Func<BannerCharacter, bool>> expression)
        {
            var bannerCharacter = await _unitOfWork.Repository<BannerCharacter>()
                .GetByAsync(expression, includeProperties: "Banner,Character,Character.Rarity,Character.ElementType");

            if (bannerCharacter == null)
                throw new NotFoundException("BannerCharacter not found.");

            return bannerCharacter.Adapt<BannerCharaterResponse>();
        }

        public async Task<BannerCharaterResponse> UpdateBannerCharacterAsync(BannerCharacterUpdateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var banner = await _unitOfWork.Repository<GachaBanners>()
                .GetByAsync(x => x.Id == request.BannerId);

            if (banner == null)
                throw new NotFoundException("Banner not found.");

            await ChackCharacterAsync(request.CharacterIds);

            List<CharacterResponse> updatedCharacters = new();

            await ExecuteWithTransactionAsync(async () =>
            {
                var existing = await _unitOfWork.Repository<BannerCharacter>()
                    .GetAllAsync(x => x.BannerId == request.BannerId);

                foreach (var item in existing)
                {
                    await _unitOfWork.Repository<BannerCharacter>().DeleteAsync(item);
                }

                foreach (var characterId in request.CharacterIds)
                {
                    var character = await _unitOfWork.Repository<Character>()
                        .GetByAsync(x => x.Id == characterId, includeProperties: "Rarity,ElementType");

                    if (character == null)
                        continue;

                    await _unitOfWork.Repository<BannerCharacter>().CreateAsync(new BannerCharacter
                    {
                        BannerId = request.BannerId,
                        CharacterId = characterId
                    });

                    updatedCharacters.Add(character.Adapt<CharacterResponse>());
                }
            });

            return new BannerCharaterResponse
            {
                BannerId = request.BannerId,
                CharacterResponses = updatedCharacters
            };
        }
    }
}
