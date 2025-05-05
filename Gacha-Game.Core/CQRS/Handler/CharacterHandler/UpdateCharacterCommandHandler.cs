using Gacha_Game.Core.CQRS.Command.CharacterCommand;
using Gacha_Game.Core.Dtos.CharacterDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.CharacterCommandHandler
{
    public class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand, CharacterResponse>
    {
        private readonly ICharacterService _characterService;
        public UpdateCharacterCommandHandler(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        public async Task<CharacterResponse> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            var response = await _characterService.UpdateCharacterAsync(request.CharacterUpdateRequest);
            return response;
        }
    }
}
