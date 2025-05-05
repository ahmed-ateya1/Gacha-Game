using Gacha_Game.Core.Dtos.BannerCharacterDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.BannerCharacterCommand
{
    public class CreateBannerCharacterCommand : IRequest<BannerCharaterResponse>
    {
        public BannerCharacterAddRequest request { get; set; }

        public CreateBannerCharacterCommand(BannerCharacterAddRequest request)
        {
            this.request = request;
        }
    }
}
