using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.GachaBannerDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.GachaBannerQueries
{
    public class GetByGachaBannerQuery : IRequest<GachaBannerResponse>
    {
        public Expression<Func<GachaBanners, bool>> Expression { get; set; }

        public GetByGachaBannerQuery(Expression<Func<GachaBanners, bool>> expression)
        {
            Expression = expression;
        }
    }
}
