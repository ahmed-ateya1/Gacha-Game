using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.CharacterDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.CharacterQueries
{
    public class GetCharacterByQuery : IRequest<CharacterResponse>
    {
        public Expression<Func<Character, bool>> Expression { get; set; }
        public GetCharacterByQuery(Expression<Func<Character, bool>> expression)
        {
            Expression = expression;
        }
    }
}
