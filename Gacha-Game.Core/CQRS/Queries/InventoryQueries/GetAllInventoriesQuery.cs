using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.InventoryDto;
using MediatR;
using System.Linq.Expressions;

namespace Gacha_Game.Core.CQRS.Queries.InventoryQueries
{
    public class GetAllInventoriesQuery : IRequest<IEnumerable<InventoryResponse>>
    {
        public Expression<Func<Inventory, bool>>? Expression { get; set; }

        public GetAllInventoriesQuery(Expression<Func<Inventory, bool>>? expression)
        {
            Expression = expression;
        }
    }
}
