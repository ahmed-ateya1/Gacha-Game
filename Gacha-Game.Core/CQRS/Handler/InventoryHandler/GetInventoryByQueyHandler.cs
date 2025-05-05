using Gacha_Game.Core.CQRS.Queries.InventoryQueries;
using Gacha_Game.Core.Dtos.InventoryDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.InventoryHandler
{
    public class GetInventoryByQueyHandler(IInventoryService _inventoryService)
        : IRequestHandler<GetInventoryByQuey, InventoryResponse>
    {
        public async Task<InventoryResponse> Handle(GetInventoryByQuey request, CancellationToken cancellationToken)
        {
            return await _inventoryService.GetInventoryByAsync(request.Expression);
        }
    }
}
