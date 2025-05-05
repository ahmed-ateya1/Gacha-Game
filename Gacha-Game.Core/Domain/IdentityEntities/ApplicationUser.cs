using Gacha_Game.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gacha_Game.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public int Currancy { get; set; } = 0;
        public int Gems { get; set; } = 0;
        public int Level { get; set; } = 1;
        public int? Exp { get; set; } = 0;
        public string? OTPCode { get; set; }
        public DateTime? OTPExpiration { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; } = [];
        public virtual ICollection<GachaPulls> GachaPulls { get; set; } = [];

    }
}
