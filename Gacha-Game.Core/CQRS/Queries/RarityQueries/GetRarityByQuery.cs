using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.RarityDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.RarityQueries
{
    public class GetRarityByQuery : IRequest<RarityResponse>
    {
        public Expression<Func<Rarity, bool>> Expression { get; set; }
        public GetRarityByQuery(Expression<Func<Rarity, bool>> expression)
        {
            Expression = expression;
        }
    }
}
