using Gacha_Game.Core.Dtos.CharacterDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.CharacterCommand
{
    public class CreateCharacterCommand : IRequest<CharacterResponse>
    {
        public CharacterAddRequest CharacterAddRequest { get; set; }

        public CreateCharacterCommand(CharacterAddRequest characterAddRequest)
        {
            CharacterAddRequest = characterAddRequest;
        }
    }
}
