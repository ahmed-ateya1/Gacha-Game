using Gacha_Game.Core.CQRS.Command.ElementTypeCommand;
using Gacha_Game.Core.Dtos.ElementTypeDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.ElementTypeHandler
{
    public class CreateElementTypeCommandHandler : IRequestHandler<CreateElementTypeCommand, ElementTypeResponse>
    {
        private readonly IElementTypeService _elementTypeService;
        public CreateElementTypeCommandHandler(IElementTypeService elementTypeService)
        {
            _elementTypeService = elementTypeService;
        }
        public async Task<ElementTypeResponse> Handle(CreateElementTypeCommand request, CancellationToken cancellationToken)
        {
            return await _elementTypeService.CreateAsync(request.elementTypeAddRequest);
        }
    }
}
