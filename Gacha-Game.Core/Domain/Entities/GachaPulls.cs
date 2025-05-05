using Gacha_Game.Core.Domain.IdentityEntities;
using Gacha_Game.Core.Helper;

namespace Gacha_Game.Core.Domain.Entities
{
    public class GachaPulls
    {
        public Guid Id { get; set; }
        public DateTime PullDate { get; set; } = DateTime.UtcNow;
        public int PullCount { get; set; }
        public PullTypes PullType { get; set; }

        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = default!;
        public Guid BannerId { get; set; }
        public virtual GachaBanners Banner { get; set; } = default!;

        public virtual ICollection<PullResult> PullResults { get; set; } = [];
    }
}
