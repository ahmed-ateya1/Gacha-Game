using MediatR;

namespace Gacha_Game.Core.CQRS.Command.CharacterCommand
{
    public class DeleteCharacterCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteCharacterCommand(Guid id)
        {
            Id = id;
        }
    }
}
