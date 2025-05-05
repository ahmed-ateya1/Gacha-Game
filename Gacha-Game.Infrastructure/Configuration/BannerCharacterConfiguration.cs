using Gacha_Game.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gacha_Game.Infrastructure.Configuration
{
    public class BannerCharacterConfiguration : IEntityTypeConfiguration<BannerCharacter>
    {
        public void Configure(EntityTypeBuilder<BannerCharacter> builder)
        {
            builder.HasKey(bc => bc.Id);


            builder.HasOne(bc => bc.Banner)
                .WithMany(b => b.BannerCharacters)
                .HasForeignKey(bc => bc.BannerId)
                .OnDelete(DeleteBehavior.NoAction); 


            builder.HasOne(bc => bc.Character)
                .WithMany(c => c.BannerCharacters)
                .HasForeignKey(bc => bc.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.ToTable("BannerCharacters");
        }
    }
}
