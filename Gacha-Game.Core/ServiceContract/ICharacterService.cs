using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.CharacterDto;
using System.Linq.Expressions;

namespace Gacha_Game.Core.ServiceContract
{
    public interface ICharacterService
    {
        Task<CharacterResponse> AddCharacterAsync(CharacterAddRequest request);
        Task<CharacterResponse> UpdateCharacterAsync(CharacterUpdateRequest request);
        Task<bool> DeleteCharacterAsync(Guid id);
        Task<CharacterResponse> GetCharacterAsync(Expression<Func<Character,bool>>expression , bool isTracked=false);
        Task<IEnumerable<CharacterResponse>> GetAllCharactersAsync(Expression<Func<Character, bool>>?expression = null);
    }
}
