using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.BannerCharacterDto;
using System.Linq.Expressions;

namespace Gacha_Game.Core.ServiceContract
{
    public interface IBannerCharacterService
    {
        Task<BannerCharaterResponse> AddBannerCharacterAsync(BannerCharacterAddRequest request);
        Task<BannerCharaterResponse> UpdateBannerCharacterAsync(BannerCharacterUpdateRequest request);

        Task<bool> DeleteBannerCharacterByIdAsync(Guid id);
        Task<BannerCharaterResponse> GetBannerCharaterByAsync(Expression<Func<BannerCharacter, bool>> expression);
        Task<IEnumerable<BannerCharaterResponse>> GetAllBannerCharacterAsync(Expression<Func<BannerCharacter,bool>>? expression);
    }
}
