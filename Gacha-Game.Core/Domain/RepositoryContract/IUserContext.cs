using Gacha_Game.Core.Domain.IdentityEntities;

namespace Gacha_Game.Core.Domain.RepositoryContract
{
    public interface IUserContext
    {
        Task<ApplicationUser?> GetCurrentUserAsync();
    }
}
