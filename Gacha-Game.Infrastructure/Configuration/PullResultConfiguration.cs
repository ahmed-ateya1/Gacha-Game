using Gacha_Game.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gacha_Game.Infrastructure.Configuration
{
    public class PullResultConfiguration : IEntityTypeConfiguration<PullResult>
    {
        public void Configure(EntityTypeBuilder<PullResult> builder)
        {
            builder.HasKey(pr => pr.Id);


            builder.HasOne(pr => pr.Pull)
                .WithMany(p => p.PullResults)
                .HasForeignKey(pr => pr.PullId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(pr => pr.Inventory)
                .WithMany(i => i.PullResults)
                .HasForeignKey(pr => pr.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PullResults");
        }
    }
}
