using Gacha_Game.Core.CQRS.Command.BannerCharacterCommand;
using Gacha_Game.Core.Dtos.BannerCharacterDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.BannerCharacterHandler
{
    public class UpdateBannerCharacterCommandHandler(IBannerCharacterService _bannerCharacter)
        : IRequestHandler<UpdateBannerCharacterCommand, BannerCharaterResponse>
    {
        public Task<BannerCharaterResponse> Handle(UpdateBannerCharacterCommand request, CancellationToken cancellationToken)
        {
            return _bannerCharacter.UpdateBannerCharacterAsync(request.request);
        }
    }
}
