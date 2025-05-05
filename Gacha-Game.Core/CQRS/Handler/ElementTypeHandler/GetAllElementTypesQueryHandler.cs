using Gacha_Game.Core.CQRS.Queries.ElementTypeQueries;
using Gacha_Game.Core.Dtos.ElementTypeDto;
using Gacha_Game.Core.ServiceContract;
using MediatR;

namespace Gacha_Game.Core.CQRS.Handler.ElementTypeHandler
{
    public class GetAllElementTypesQueryHandler(IElementTypeService _elementTypeService)
        : IRequestHandler<GetAllElementTypesQuery, IEnumerable<ElementTypeResponse>>
    {
        public async Task<IEnumerable<ElementTypeResponse>> Handle(GetAllElementTypesQuery request, CancellationToken cancellationToken)
        {
           return await _elementTypeService.GetAllAsync(request.Filter);
        }
    }
}
