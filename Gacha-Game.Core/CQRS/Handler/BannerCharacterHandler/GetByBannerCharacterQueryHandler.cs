using Gacha_Game.Core.CQRS.Queries.BannerCharacterQueries;
using Gacha_Game.Core.Dtos.BannerCharacterDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.BannerCharacterHandler
{
    public class GetByBannerCharacterQueryHandler(IBannerCharacterService _bannerCharacterService)
        : IRequestHandler<GetByBannerCharacterQuery, BannerCharaterResponse>
    {
        public async Task<BannerCharaterResponse> Handle(GetByBannerCharacterQuery request, CancellationToken cancellationToken)
        {
            return await _bannerCharacterService
                .GetBannerCharaterByAsync(request.Expression);
        }
    }
}
