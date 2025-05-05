using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.CharacterDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.CharacterQueries
{
    public class GetAllCharacterQury : IRequest<IEnumerable<CharacterResponse>>
    {
        public Expression<Func<Character, bool>>? Expression { get; set; }
        public GetAllCharacterQury(Expression<Func<Character, bool>>? expression)
        {
            Expression = expression;
        }
    }
}
