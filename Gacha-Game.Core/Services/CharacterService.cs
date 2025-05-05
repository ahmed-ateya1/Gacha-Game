using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Domain.RepositoryContract;
using Gacha_Game.Core.Dtos.CharacterDto;
using Gacha_Game.Core.Execptions;
using Gacha_Game.Core.ServiceContract;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Gacha_Game.Core.Services
{
    public class CharacterService(
        IUnitOfWork _unitOfWork , 
        IFileServices _fileServices, 
        ILogger<CharacterService> _logger
        ): ICharacterService
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
        private async Task<(Rarity, ElementType)> CheckValidateElementAndRarity(Guid rarityID, Guid elementTypeID)
        {
            var rarity = await _unitOfWork.Repository<Rarity>().GetByAsync(x => x.Id == rarityID);
            var elementType = await _unitOfWork.Repository<ElementType>().GetByAsync(x => x.Id == elementTypeID);

           
            if (rarity == null)
                throw new NotFoundException("Rarity not found");
            if (elementType == null)
                throw new NotFoundException("Element Type not found");

            return (rarity, elementType);
        }

       
        public async Task<CharacterResponse> AddCharacterAsync(CharacterAddRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var (rarity, elementType) = await CheckValidateElementAndRarity(request.RarityId, request.ElementTypeId);

            
            var character = request.Adapt<Character>();
            character.Id = Guid.NewGuid();
            character.Rarity = rarity;
            character.ElementType = elementType;

            if (request.Image != null)
            {
                var imageUrl = await _fileServices.CreateFile(request.Image);
                character.ImageUrl = imageUrl;
            }
            else
            {
                throw new ArgumentNullException(nameof(request.Image));
            }

            _logger.LogInformation("Adding new character with ID: {Id}", character.Id);
            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<Character>().CreateAsync(character);
            });
            _logger.LogInformation("Character added successfully.");

            return character.Adapt<CharacterResponse>();
        }

        public async Task<bool> DeleteCharacterAsync(Guid id)
        {
            var character = await _unitOfWork.Repository<Character>()
                .GetByAsync(x => x.Id == id,includeProperties: "Rarity,ElementType,Inventories,BannerCharacters");

            if (character == null)
            {
                throw new NotFoundException("Character not found");
            }
            _logger.LogInformation("Deleting character with ID: {Id}", id);
            await ExecuteWithTransactionAsync(async () =>
            {
                if (character.Inventories != null && character.Inventories.Any())
                {
                    foreach (var inventory in character.Inventories)
                    {
                        await _unitOfWork.Repository<Inventory>().DeleteAsync(inventory);
                    }
                }
                if (character.BannerCharacters != null && character.BannerCharacters.Any())
                {
                    foreach (var bannerCharacter in character.BannerCharacters)
                    {
                        await _unitOfWork.Repository<BannerCharacter>().DeleteAsync(bannerCharacter);
                    }
                }
                if (character.ImageUrl != null)
                {
                    await _fileServices.DeleteFile(character.ImageUrl);
                }
                await _unitOfWork.Repository<Character>().DeleteAsync(character);
            });
            _logger.LogInformation("Character deleted successfully.");
            return true;
        }

        public async Task<IEnumerable<CharacterResponse>> GetAllCharactersAsync(Expression<Func<Character, bool>>? expression = null)
        {
            var characters = await _unitOfWork.Repository<Character>()
                .GetAllAsync(expression, includeProperties: "Rarity,ElementType");
            if (characters == null || !characters.Any())
            {
                _logger.LogWarning("No characters found.");
                return Enumerable.Empty<CharacterResponse>();
            }
            _logger.LogInformation("Retrieved {Count} characters.", characters.Count());
            return characters.Adapt<IEnumerable<CharacterResponse>>();
        }

        public async Task<CharacterResponse> GetCharacterAsync(Expression<Func<Character, bool>> expression, bool isTracked = false)
        {
            var character =  await _unitOfWork.Repository<Character>()
                .GetByAsync(expression, isTracked, includeProperties: "Rarity,ElementType");

            if (character == null)
            {
                throw new NotFoundException("Character not found");
            }
            _logger.LogInformation("Retrieved character with ID: {Id}", character.Id);

            return character.Adapt<CharacterResponse>();
        }

        public async Task<CharacterResponse> UpdateCharacterAsync(CharacterUpdateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var character = await _unitOfWork.Repository<Character>()
                .GetByAsync(x => x.Id == request.Id, includeProperties: "Rarity,ElementType");
            if (character == null)
                throw new NotFoundException("Character not found");

            var (rarity, elementType) = await CheckValidateElementAndRarity(request.RarityId, request.ElementTypeId);

            request.Adapt(character);
            character.Rarity = rarity;
            character.ElementType = elementType;

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(character.ImageUrl))
                    await _fileServices.DeleteFile(character.ImageUrl);
                character.ImageUrl = await _fileServices.CreateFile(request.Image);
            }

            _logger.LogInformation("Updating character with ID: {Id}", character.Id);
            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<Character>().UpdateAsync(character);
            });
            _logger.LogInformation("Character updated successfully.");

            return character.Adapt<CharacterResponse>(); 
        }
    }
}
