using Gacha_Game.Core.Dtos.GachaBannerDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.GachaBannerCommand
{
    public class CreateGachaBannerCommand : IRequest<GachaBannerResponse>
    {
        public GachaBannerAddRequest Request { get; set; }

        public CreateGachaBannerCommand(GachaBannerAddRequest request)
        {
            Request = request;
        }
    }
}
