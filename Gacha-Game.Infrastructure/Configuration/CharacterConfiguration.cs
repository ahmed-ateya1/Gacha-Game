using Gacha_Game.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gacha_Game.Infrastructure.Configuration
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c=>c.ImageUrl)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c=>c.AttackBase)
                .IsRequired();

            builder.Property(c => c.DefenseBase)
                .IsRequired();

            builder.Property(c => c.HealthBase)
                .IsRequired();

            builder.HasOne(c => c.Rarity)
                .WithMany(r => r.Characters)
                .HasForeignKey(c => c.RarityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.ElementType)
                .WithMany(c=>c.Characters)
                .HasForeignKey(c => c.ElementTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Characters"); 
        }
    }
}
