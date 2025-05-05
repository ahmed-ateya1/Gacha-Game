using Gacha_Game.Core.CQRS.Queries.BannerCharacterQueries;
using Gacha_Game.Core.Dtos.BannerCharacterDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.BannerCharacterHandler
{
    public class GetAllBannerCharacterQueryHandler(IBannerCharacterService _bannerCharacterService)
        : IRequestHandler<GetAllBannerCharacterQuery, IEnumerable<BannerCharaterResponse>>
    {
        public async Task<IEnumerable<BannerCharaterResponse>> Handle(GetAllBannerCharacterQuery request, CancellationToken cancellationToken)
        {
            return await _bannerCharacterService
                .GetAllBannerCharacterAsync(request.Expression);
        }
    }
}
