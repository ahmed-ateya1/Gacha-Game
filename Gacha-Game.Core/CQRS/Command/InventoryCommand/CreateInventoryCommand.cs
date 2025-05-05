using Gacha_Game.Core.Dtos.InventoryDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.InventoryCommand
{
    public class CreateInventoryCommand : IRequest<InventoryResponse>
    {
        public InventoryAddRequest? Request { get; set; }
        public CreateInventoryCommand(InventoryAddRequest? request)
        {
            Request = request;
        }
    }
}
