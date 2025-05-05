using Gacha_Game.Core.Domain.IdentityEntities;

namespace Gacha_Game.Core.Domain.Entities
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public DateTime ObtainedDate { get; set; } = DateTime.UtcNow;
        public Guid UserID { get; set; }
        public virtual ApplicationUser User { get; set; } = default!;
        public Guid CharacterID { get; set; }
        public virtual Character Character { get; set; } = default!;
        public virtual ICollection<PullResult> PullResults { get; set; } = [];

    }
}
