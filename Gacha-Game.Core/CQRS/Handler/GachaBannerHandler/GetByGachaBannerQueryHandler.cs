using Gacha_Game.Core.CQRS.Queries.GachaBannerQueries;
using Gacha_Game.Core.Dtos.GachaBannerDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.GachaBannerHandler
{
    public class GetByGachaBannerQueryHandler(IGachaBannerService gachaBannerService) :
        IRequestHandler<GetByGachaBannerQuery, GachaBannerResponse>
    {
        public async Task<GachaBannerResponse> Handle(GetByGachaBannerQuery request, CancellationToken cancellationToken)
        {
            return await gachaBannerService.GetGachaBannerByAsync(request.Expression);
        }
    }

}
