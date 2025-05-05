using Gacha_Game.Core.CQRS.Command.ElementTypeCommand;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.ElementTypeHandler
{
    public class DeleteElementTypeCommandHandler : IRequestHandler<DeleteElementTypeCommand, bool>
    {
        private readonly IElementTypeService _elementTypeService;
        public DeleteElementTypeCommandHandler(IElementTypeService elementTypeService)
        {
            _elementTypeService = elementTypeService;
        }
        public async Task<bool> Handle(DeleteElementTypeCommand request, CancellationToken cancellationToken)
        {
            return await _elementTypeService.DeleteAsync(request.Id);
        }
    }
}
