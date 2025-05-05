using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.ElementTypeDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.ElementTypeQueries
{
    public class GetAllElementTypesQuery : IRequest<IEnumerable<ElementTypeResponse>>
    {
        public Expression<Func<ElementType,bool>>? Filter { get; set; }

        public GetAllElementTypesQuery(Expression<Func<ElementType, bool>>? filter)
        {
            Filter = filter;
        }
    }
}
