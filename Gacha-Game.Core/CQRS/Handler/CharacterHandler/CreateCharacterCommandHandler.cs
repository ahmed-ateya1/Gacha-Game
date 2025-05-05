using Gacha_Game.Core.CQRS.Command.CharacterCommand;
using Gacha_Game.Core.Dtos.CharacterDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.CharacterCommandHandler
{
    public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, CharacterResponse>
    {
        private readonly ICharacterService _characterService;
        public CreateCharacterCommandHandler(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        public async Task<CharacterResponse> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var response = await _characterService.AddCharacterAsync(request.CharacterAddRequest);
            return response;
        }
    }
}
