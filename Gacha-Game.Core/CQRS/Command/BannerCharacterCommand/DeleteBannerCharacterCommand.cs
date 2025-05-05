using MediatR;

namespace Gacha_Game.Core.CQRS.Command.BannerCharacterCommand
{
    public class DeleteBannerCharacterCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteBannerCharacterCommand(Guid id)
        {
            Id = id;
        }
    }
}
