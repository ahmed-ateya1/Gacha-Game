using Gacha_Game.Core.CQRS.Queries.GachaBannerQueries;
using Gacha_Game.Core.Dtos.GachaBannerDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.GachaBannerHandler
{
    public class GetAllGachaBannerQueryHandler(IGachaBannerService gachaBannerService) :
        IRequestHandler<GetAllGachaBannerQuery, IEnumerable<GachaBannerResponse>>
    {
        public async Task<IEnumerable<GachaBannerResponse>> Handle(GetAllGachaBannerQuery request, CancellationToken cancellationToken)
        {
            return await gachaBannerService.GetAllGachaBanners(request.Expression);
        }
    }

}
