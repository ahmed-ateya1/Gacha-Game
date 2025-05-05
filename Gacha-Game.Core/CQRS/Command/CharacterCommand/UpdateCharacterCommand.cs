using Gacha_Game.Core.Dtos.CharacterDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.CharacterCommand
{
    public class UpdateCharacterCommand : IRequest<CharacterResponse>
    {
        public CharacterUpdateRequest CharacterUpdateRequest { get; set; }
        public UpdateCharacterCommand(CharacterUpdateRequest characterUpdateRequest)
        {
            CharacterUpdateRequest = characterUpdateRequest;
        }
    }
}
