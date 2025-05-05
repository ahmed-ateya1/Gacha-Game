using Gacha_Game.Core.CQRS.Queries.RarityQueries;
using Gacha_Game.Core.Dtos.RarityDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.RarityQueryHandler
{
    public class GetRarityByQueryHandler : IRequestHandler<GetRarityByQuery, RarityResponse>
    {
        private readonly IRarityService _rarityService;
        public GetRarityByQueryHandler(IRarityService rarityService)
        {
            _rarityService = rarityService;
        }
        public async Task<RarityResponse> Handle(GetRarityByQuery request, CancellationToken cancellationToken)
        {
            var result = await _rarityService.GetRarityByAsync(request.Expression);
            return result;
        }
    }
}
