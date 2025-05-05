using Gacha_Game.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gacha_Game.Infrastructure.Configuration
{
    public class RarityConfiguration : IEntityTypeConfiguration<Rarity>
    {
        public void Configure(EntityTypeBuilder<Rarity> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.ColorCode)
                .HasMaxLength(8);
            builder.Property(r => r.DropRate)
                .IsRequired();

            builder.ToTable("Rarities");
        }
    }
}
