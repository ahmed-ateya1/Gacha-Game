using Gacha_Game.Core.CQRS.Command.RarityCommand;
using Gacha_Game.Core.Dtos.RarityDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.RarityQueryHandler
{
    public class CreateRarityCommandHandler : IRequestHandler<CreateRarityCommand, RarityResponse>
    {
        private readonly IRarityService _rarityService;
        public CreateRarityCommandHandler(IRarityService rarityService)
        {
            _rarityService = rarityService;
        }
        public async Task<RarityResponse> Handle(CreateRarityCommand request, CancellationToken cancellationToken)
        {
            var result = await _rarityService.AddRarityAsync(request.RarityAddRequest);
            return result;
        }
    }
}
