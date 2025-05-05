using Gacha_Game.Core.CQRS.Queries.CharacterQueries;
using Gacha_Game.Core.Dtos.CharacterDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.CharacterCommandHandler
{
    public class GetCharacterByQueryHandler(ICharacterService _characterService)
        : IRequestHandler<GetCharacterByQuery, CharacterResponse>
    {
        public async Task<CharacterResponse> Handle(GetCharacterByQuery request, CancellationToken cancellationToken)
        {
            var response = await _characterService.GetCharacterAsync(request.Expression);
            return response;
        }
    }
}
