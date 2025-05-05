using Gacha_Game.Core.CQRS.Queries.InventoryQueries;
using Gacha_Game.Core.Dtos.InventoryDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.InventoryHandler
{
    public class GetAllInventoriesQueryHandler(IInventoryService _inventoryService)
        : IRequestHandler<GetAllInventoriesQuery, IEnumerable<InventoryResponse>>
    {
       
        public async Task<IEnumerable<InventoryResponse>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            return await _inventoryService
                .GetInventoriesAsync(request.Expression);
        }
    }
}
