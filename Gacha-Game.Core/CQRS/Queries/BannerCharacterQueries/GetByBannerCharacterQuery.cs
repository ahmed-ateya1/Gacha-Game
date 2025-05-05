using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.BannerCharacterDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.BannerCharacterQueries
{
    public class GetByBannerCharacterQuery : IRequest<BannerCharaterResponse>
    {
        public Expression<Func<BannerCharacter, bool>> Expression { get; set; }

        public GetByBannerCharacterQuery(Expression<Func<BannerCharacter, bool>> expression)
        {
            Expression = expression;
        }
    }
}
