using Gacha_Game.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gacha_Game.Infrastructure.Configuration
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(x=>x.Id);


            builder.Property(i => i.ObtainedDate)
                .IsRequired();

            builder.HasOne(i => i.User)
                .WithMany(u => u.Inventories)
                .HasForeignKey(i => i.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Character)
                .WithMany(c => c.Inventories)
                .HasForeignKey(i => i.CharacterID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Inventories");
        }
    }
}
