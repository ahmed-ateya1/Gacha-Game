using Gacha_Game.Core.CQRS.Queries.RarityQueries;
using Gacha_Game.Core.Dtos.RarityDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.RarityQueryHandler
{
    public class GetAllRarityQueryHandler : IRequestHandler<GetAllRarityQuery, IEnumerable<RarityResponse>>
    {
        private readonly IRarityService _rarityService;
        public GetAllRarityQueryHandler(IRarityService rarityService)
        {
            _rarityService = rarityService;
        }
        public async Task<IEnumerable<RarityResponse>> Handle(GetAllRarityQuery request, CancellationToken cancellationToken)
        {
            var result = await _rarityService.GetAllRaritiesAsync(request.Expression);
            return result;
        }
    }
}
