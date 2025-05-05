using Gacha_Game.Core.Dtos.GachaBannerDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.GachaBannerCommand
{
    public class UpdateGachaBannerCommand : IRequest<GachaBannerResponse>
    {
        public GachaBannerUpdateRequest Request { get; set; }
        public UpdateGachaBannerCommand(GachaBannerUpdateRequest request)
        {
            Request = request;
        }
    }
}
