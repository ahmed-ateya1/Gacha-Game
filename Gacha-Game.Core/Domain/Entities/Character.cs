namespace Gacha_Game.Core.Domain.Entities
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int AttackBase { get; set; }
        public int DefenseBase { get; set; }
        public int HealthBase { get; set; }
        public Guid RarityId { get; set; }
        public virtual Rarity Rarity { get; set; } = default!;
        public Guid ElementTypeId { get; set; }
        public virtual ElementType ElementType { get; set; } = default!;

        public virtual ICollection<Inventory> Inventories { get; set; } = [];
        public virtual ICollection<BannerCharacter> BannerCharacters { get; set; } = [];
    }
}
