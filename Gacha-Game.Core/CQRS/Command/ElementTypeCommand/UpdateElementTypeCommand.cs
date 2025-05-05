using Gacha_Game.Core.Dtos.ElementTypeDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.ElementTypeCommand
{
    public class UpdateElementTypeCommand : IRequest<ElementTypeResponse>
    {
        public ElementTypeUpdateRequest elementTypeUpdateRequest { get; set; }
        public UpdateElementTypeCommand(ElementTypeUpdateRequest elementTypeUpdateRequest)
        {
            this.elementTypeUpdateRequest = elementTypeUpdateRequest;
        }
    }
}
