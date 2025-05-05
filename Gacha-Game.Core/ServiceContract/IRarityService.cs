using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.RarityDto;
using System.Linq.Expressions;

namespace Gacha_Game.Core.ServiceContract
{
    public interface IRarityService
    {
        Task<RarityResponse> AddRarityAsync(RarityAddRequest request);
        Task<RarityResponse> UpdateRarityAsync(RarityUpdateRequest request);

        Task<bool> DeleteRarityAsync(Guid id);

        Task<RarityResponse?> GetRarityByAsync(Expression<Func<Rarity,bool>> expression,bool isTracked=false);

        Task<IEnumerable<RarityResponse>> GetAllRaritiesAsync(Expression<Func<Rarity,bool>>? expression = null);
    }
}
