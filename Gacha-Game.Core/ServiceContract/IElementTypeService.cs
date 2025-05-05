using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.ElementTypeDto;
using System.Linq.Expressions;

namespace Gacha_Game.Core.ServiceContract
{
    public interface IElementTypeService
    {
        Task<ElementTypeResponse> CreateAsync(ElementTypeAddRequest? request);
        Task<ElementTypeResponse> UpdateAsync(ElementTypeUpdateRequest? request);
        Task<bool> DeleteAsync(Guid id);
        Task<ElementTypeResponse> GetByAsync(Expression<Func<ElementType, bool>> predicate,bool isTracked=false);
        Task<IEnumerable<ElementTypeResponse>> GetAllAsync(Expression<Func<ElementType, bool>>? predicate = null);

    }
}
