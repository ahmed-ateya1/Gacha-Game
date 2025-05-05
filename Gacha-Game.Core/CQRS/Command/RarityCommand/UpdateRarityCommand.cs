using Gacha_Game.Core.Dtos.RarityDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.RarityCommand
{
    public class UpdateRarityCommand : IRequest<RarityResponse>
    {
        public RarityUpdateRequest RarityUpdateRequest { get; set; }
        public UpdateRarityCommand(RarityUpdateRequest rarityUpdateRequest)
        {
            RarityUpdateRequest = rarityUpdateRequest;
        }
    }
}
