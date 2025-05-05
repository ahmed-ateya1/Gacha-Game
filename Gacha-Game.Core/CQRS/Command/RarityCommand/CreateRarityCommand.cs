using Gacha_Game.Core.Dtos.RarityDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.RarityCommand
{
    public class CreateRarityCommand : IRequest<RarityResponse>
    {
        public RarityAddRequest RarityAddRequest { get; set; }

        public CreateRarityCommand(RarityAddRequest rarityAddRequest)
        {
            RarityAddRequest = rarityAddRequest;
        }
    }
}
