using Gacha_Game.Core.CQRS.Command.BannerCharacterCommand;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.BannerCharacterHandler
{
    public class DeleteBannerCharacterCommandHandler(IBannerCharacterService _bannerCharacter)
        : IRequestHandler<DeleteBannerCharacterCommand, bool>
    {
        public Task<bool> Handle(DeleteBannerCharacterCommand request, CancellationToken cancellationToken)
        {
            return _bannerCharacter.DeleteBannerCharacterByIdAsync(request.Id);
        }
    }
}
