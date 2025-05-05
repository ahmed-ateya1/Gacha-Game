using Gacha_Game.Core.CQRS.Queries.ElementTypeQueries;
using Gacha_Game.Core.Dtos.ElementTypeDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.ElementTypeHandler
{
    public class GetElementTypeByQueryHandler : IRequestHandler<GetElementTypeByQuery, ElementTypeResponse>
    {
        private readonly IElementTypeService _elementTypeService;

        public GetElementTypeByQueryHandler(IElementTypeService elementTypeService)
        {
            _elementTypeService = elementTypeService;
        }

        public async Task<ElementTypeResponse> Handle(GetElementTypeByQuery request, CancellationToken cancellationToken)
        {
            if (request == null || request.predicate == null)
                throw new ArgumentNullException(nameof(request));

            var response = await _elementTypeService.GetByAsync(request.predicate);

            return response;
        }
    }
}
