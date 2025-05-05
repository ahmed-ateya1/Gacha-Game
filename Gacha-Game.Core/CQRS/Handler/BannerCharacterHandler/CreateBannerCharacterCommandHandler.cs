using Gacha_Game.Core.CQRS.Command.BannerCharacterCommand;
using Gacha_Game.Core.Dtos.BannerCharacterDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.BannerCharacterHandler
{
    public class CreateBannerCharacterCommandHandler(IBannerCharacterService _bannerCharacter)
        : IRequestHandler<CreateBannerCharacterCommand, BannerCharaterResponse>
    {
        public async Task<BannerCharaterResponse> Handle(CreateBannerCharacterCommand request, CancellationToken cancellationToken)
        {
            return await _bannerCharacter.AddBannerCharacterAsync(request.request);
        }
    }
}
