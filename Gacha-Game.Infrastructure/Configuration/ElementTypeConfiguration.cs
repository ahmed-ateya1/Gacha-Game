using Gacha_Game.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gacha_Game.Infrastructure.Configuration
{
    public class ElementTypeConfiguration : IEntityTypeConfiguration<ElementType>
    {
        public void Configure(EntityTypeBuilder<ElementType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.IconUrl)
                .HasMaxLength(200);

            builder.ToTable("ElementTypes");
        }
    }
}
