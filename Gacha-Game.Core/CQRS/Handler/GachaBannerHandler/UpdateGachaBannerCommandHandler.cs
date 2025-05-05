using Gacha_Game.Core.CQRS.Command.GachaBannerCommand;
using Gacha_Game.Core.Dtos.GachaBannerDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.GachaBannerHandler
{
    public class UpdateGachaBannerCommandHandler : IRequestHandler<UpdateGachaBannerCommand, GachaBannerResponse>
    {
        private readonly IGachaBannerService _gachaBannerService;
        public UpdateGachaBannerCommandHandler(IGachaBannerService gachaBannerService)
        {
            _gachaBannerService = gachaBannerService;
        }
        public async Task<GachaBannerResponse> Handle(UpdateGachaBannerCommand request, CancellationToken cancellationToken)
        {
            return await _gachaBannerService.UpdateGachaBannerAsync(request.Request);
        }
    }
}
