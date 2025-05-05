using MediatR;

namespace Gacha_Game.Core.CQRS.Command.GachaBannerCommand
{
    public class DeleteGachaBannerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteGachaBannerCommand(Guid id)
        {
            Id = id;
        }
    }
}
