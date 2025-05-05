using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.GachaBannerDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.GachaBannerQueries
{
    public class GetAllGachaBannerQuery : IRequest<IEnumerable<GachaBannerResponse>>
    {
        public Expression<Func<GachaBanners, bool>>? Expression { get; set; }
        public GetAllGachaBannerQuery(Expression<Func<GachaBanners, bool>>? expression)
        {
            Expression = expression;
        }
    }
}
