using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Domain.IdentityEntities;
using Gacha_Game.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gacha_Game.Infrastructure.Data
{
    public class GachaDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public GachaDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<ElementType> ElementTypes { get; set; }
        public DbSet<Rarity> Rarities { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<GachaBanners> GachaBanners { get; set; }
        public DbSet<BannerCharacter> BannerCharacters { get; set; }
        public DbSet<GachaPulls> GachaPulls { get; set; }
        public DbSet<PullResult> PullResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(CharacterConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
