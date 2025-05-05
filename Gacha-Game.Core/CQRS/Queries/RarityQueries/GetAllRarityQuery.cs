using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.RarityDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.RarityQueries
{
    public class GetAllRarityQuery : IRequest<IEnumerable<RarityResponse>>
    {
        public Expression<Func<Rarity, bool>>? Expression { get; set; }

        public GetAllRarityQuery(Expression<Func<Rarity, bool>>? expression)
        {
            Expression = expression;
        }
    }
}
