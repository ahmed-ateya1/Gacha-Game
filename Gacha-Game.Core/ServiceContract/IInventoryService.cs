using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.InventoryDto;
using System.Linq.Expressions;

namespace Gacha_Game.Core.ServiceContract
{
    public interface IInventoryService
    {
        Task<InventoryResponse> AddInventoryAsync(InventoryAddRequest? request);
        Task<InventoryResponse> GetInventoryByAsync(Expression<Func<Inventory, bool>> expression);
        Task<IEnumerable<InventoryResponse>> GetInventoriesAsync(Expression<Func<Inventory, bool>>? expression = null);

    }
}
