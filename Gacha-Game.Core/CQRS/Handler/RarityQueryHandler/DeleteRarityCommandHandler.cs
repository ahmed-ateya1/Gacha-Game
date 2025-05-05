using Gacha_Game.Core.CQRS.Command.RarityCommand;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.RarityQueryHandler
{
    public class DeleteRarityCommandHandler : IRequestHandler<DeleteRarityCommand, bool>
    {
        private readonly IRarityService _rarityService;
        public DeleteRarityCommandHandler(IRarityService rarityService)
        {
            _rarityService = rarityService;
        }
        public async Task<bool> Handle(DeleteRarityCommand request, CancellationToken cancellationToken)
        {
            var result = await _rarityService.DeleteRarityAsync(request.Id);
            return result;
        }
    }
}
