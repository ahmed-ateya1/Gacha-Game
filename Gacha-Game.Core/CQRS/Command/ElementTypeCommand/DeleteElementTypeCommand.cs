using MediatR;

namespace Gacha_Game.Core.CQRS.Command.ElementTypeCommand
{
    public class DeleteElementTypeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteElementTypeCommand(Guid id)
        {
            Id = id;
        }
    }
}
