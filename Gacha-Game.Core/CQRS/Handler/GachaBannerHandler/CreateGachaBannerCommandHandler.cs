using Gacha_Game.Core.CQRS.Command.GachaBannerCommand;
using Gacha_Game.Core.Dtos.GachaBannerDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.GachaBannerHandler
{
    public class CreateGachaBannerCommandHandler : IRequestHandler<CreateGachaBannerCommand, GachaBannerResponse>
    {
        private readonly IGachaBannerService _gachaBannerService;
        public CreateGachaBannerCommandHandler(IGachaBannerService gachaBannerService)
        {
            _gachaBannerService = gachaBannerService;
        }
        public async Task<GachaBannerResponse> Handle(CreateGachaBannerCommand request, CancellationToken cancellationToken)
        {
            return await _gachaBannerService.AddGachaBannerAsync(request.Request);
        }
    }
}
