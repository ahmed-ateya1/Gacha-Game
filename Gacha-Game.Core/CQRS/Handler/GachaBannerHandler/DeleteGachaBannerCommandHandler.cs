using Gacha_Game.Core.CQRS.Command.GachaBannerCommand;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.GachaBannerHandler
{
    public class DeleteGachaBannerCommandHandler : IRequestHandler<DeleteGachaBannerCommand, bool>
    {
        private readonly IGachaBannerService _gachaBannerService;
        public DeleteGachaBannerCommandHandler(IGachaBannerService gachaBannerService)
        {
            _gachaBannerService = gachaBannerService;
        }
        public async Task<bool> Handle(DeleteGachaBannerCommand request, CancellationToken cancellationToken)
        {
            return await _gachaBannerService.DeleteGachaBannerAsync(request.Id);
        }
    }
}
