using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.ElementTypeDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.ElementTypeQueries
{
    public class GetElementTypeByQuery : IRequest<ElementTypeResponse>
    {
        public Expression<Func<ElementType, bool>> predicate;

        public GetElementTypeByQuery(Expression<Func<ElementType, bool>> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }
    }
}
