using Gacha_Game.Core.CQRS.Command.RarityCommand;
using Gacha_Game.Core.Dtos.RarityDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.RarityQueryHandler
{
    public class UpdateRarityCommandHandler : IRequestHandler<UpdateRarityCommand, RarityResponse>
    {
        private readonly IRarityService _rarityService;
        public UpdateRarityCommandHandler(IRarityService rarityService)
        {
            _rarityService = rarityService;
        }
        public async Task<RarityResponse> Handle(UpdateRarityCommand request, CancellationToken cancellationToken)
        {
            var result = await _rarityService.UpdateRarityAsync(request.RarityUpdateRequest);
            return result;
        }

    }
}
