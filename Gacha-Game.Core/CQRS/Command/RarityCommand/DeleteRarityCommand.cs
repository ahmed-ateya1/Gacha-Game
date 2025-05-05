using MediatR;

namespace Gacha_Game.Core.CQRS.Command.RarityCommand
{
    public class DeleteRarityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteRarityCommand(Guid id)
        {
            Id = id;
        }
    }
}
