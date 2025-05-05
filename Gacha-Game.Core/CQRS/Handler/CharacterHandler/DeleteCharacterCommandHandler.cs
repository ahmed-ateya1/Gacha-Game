using Gacha_Game.Core.CQRS.Command.CharacterCommand;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.CharacterCommandHandler
{
    public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, bool>
    {
        private readonly ICharacterService _characterService;
        public DeleteCharacterCommandHandler(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        public async Task<bool> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var response = await _characterService.DeleteCharacterAsync(request.Id);
            return response;
        }
    }
}
