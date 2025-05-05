using Gacha_Game.Core.CQRS.Command.InventoryCommand;
using Gacha_Game.Core.Dtos.InventoryDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.InventoryHandler
{
    public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, InventoryResponse>
    {
        private readonly IInventoryService _inventoryService;
        public CreateInventoryCommandHandler(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        public async Task<InventoryResponse> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            return await _inventoryService.AddInventoryAsync(request.Request);
        }
    }
}
