using Gacha_Game.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gacha_Game.Infrastructure.Configuration
{
    public class GachaBannerConfiguration : IEntityTypeConfiguration<GachaBanners>
    {
        public void Configure(EntityTypeBuilder<GachaBanners> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(b => b.StartDate)
                .IsRequired();

            builder.Property(b => b.EndDate)
                .IsRequired();

            builder.Property(b => b.CostPerPull)
                .IsRequired();

            builder.Property(b => b.PullsForGuaranteedRarity)
                .IsRequired();

            builder.Property(b => b.BannerImageUrl)
                .IsRequired()
                .HasMaxLength(200);



            builder.ToTable("GachaBanners");

        }
    }
}
