using Gacha_Game.Core.CQRS.Queries.CharacterQueries;
using Gacha_Game.Core.Dtos.CharacterDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.CharacterCommandHandler
{
    public class GetAllCharacterQuryHandler(ICharacterService _characterService)
        : IRequestHandler<GetAllCharacterQury, IEnumerable<CharacterResponse>>
    {
        public async Task<IEnumerable<CharacterResponse>> Handle(GetAllCharacterQury request, CancellationToken cancellationToken)
        {
            var response = await _characterService.GetAllCharactersAsync(request.Expression);
            return response;
        }
    }
}
