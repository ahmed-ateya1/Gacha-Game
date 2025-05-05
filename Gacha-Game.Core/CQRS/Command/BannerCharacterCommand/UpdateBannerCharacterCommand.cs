using Gacha_Game.Core.Dtos.BannerCharacterDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.BannerCharacterCommand
{
    public class UpdateBannerCharacterCommand : IRequest<BannerCharaterResponse>
    {
        public BannerCharacterUpdateRequest request { get; set; }
        public UpdateBannerCharacterCommand(BannerCharacterUpdateRequest request)
        {
            this.request = request;
        }
    }
}
