using Gacha_Game.Core.CQRS.Command.ElementTypeCommand;
using Gacha_Game.Core.Dtos.ElementTypeDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.ElementTypeHandler
{
    public class UpdateElementTypeCommandHandler : IRequestHandler<UpdateElementTypeCommand, ElementTypeResponse>
    {
        private readonly IElementTypeService _elementTypeService;
        public UpdateElementTypeCommandHandler(IElementTypeService elementTypeService)
        {
            _elementTypeService = elementTypeService;
        }
        public async Task<ElementTypeResponse> Handle(UpdateElementTypeCommand request, CancellationToken cancellationToken)
        {
            return await _elementTypeService.UpdateAsync(request.elementTypeUpdateRequest);
        }
    }
}
