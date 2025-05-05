using Gacha_Game.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gacha_Game.Infrastructure.Configuration
{
    public class GachaPullsConfiguration : IEntityTypeConfiguration<GachaPulls>
    {
        public void Configure(EntityTypeBuilder<GachaPulls> builder)
        {
            builder.HasKey(gp => gp.Id);

            builder.Property(gp => gp.PullDate)
                .IsRequired();

            builder.Property(gp => gp.PullCount)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(gp => gp.PullType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);


            builder.HasOne(gp => gp.User)
                .WithMany(u => u.GachaPulls)
                .HasForeignKey(gp => gp.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(gp => gp.Banner)
                .WithMany(b => b.GachaPulls)
                .HasForeignKey(gp => gp.BannerId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.ToTable("GachaPulls");
        }
    }
}
