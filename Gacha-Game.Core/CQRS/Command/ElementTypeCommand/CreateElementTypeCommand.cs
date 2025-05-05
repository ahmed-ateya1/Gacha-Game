using Gacha_Game.Core.Dtos.ElementTypeDto;
using MediatR;

namespace Gacha_Game.Core.CQRS.Command.ElementTypeCommand
{
    public class CreateElementTypeCommand : IRequest<ElementTypeResponse>
    {
        public ElementTypeAddRequest elementTypeAddRequest { get; set; }

        public CreateElementTypeCommand(ElementTypeAddRequest elementTypeAddRequest)
        {
            this.elementTypeAddRequest = elementTypeAddRequest;
        }
    }
}
