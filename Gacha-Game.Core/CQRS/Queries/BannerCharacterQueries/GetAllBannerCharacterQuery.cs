using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.BannerCharacterDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.BannerCharacterQueries
{
    public class GetAllBannerCharacterQuery : IRequest<IEnumerable<BannerCharaterResponse>>
    {
        public Expression<Func<BannerCharacter, bool>>? Expression { get; set; }
        public GetAllBannerCharacterQuery(Expression<Func<BannerCharacter, bool>>? expression = null)
        {
            Expression = expression;
        }
    }
}
