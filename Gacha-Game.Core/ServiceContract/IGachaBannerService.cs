using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.GachaBannerDto;
using System.Linq.Expressions;

namespace Gacha_Game.Core.ServiceContract
{
    public interface IGachaBannerService
    {
        Task<GachaBannerResponse> AddGachaBannerAsync(GachaBannerAddRequest? request);
        Task<GachaBannerResponse> UpdateGachaBannerAsync(GachaBannerUpdateRequest? request);
        Task<bool> DeleteGachaBannerAsync(Guid id);
        Task<GachaBannerResponse?> GetGachaBannerByAsync(Expression<Func<GachaBanners,bool>> expression);
        Task<IEnumerable<GachaBannerResponse>> GetAllGachaBanners(Expression<Func<GachaBanners, bool>>? expression = null);
    }
}
