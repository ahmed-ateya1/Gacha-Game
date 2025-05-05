namespace Gacha_Game.Core.Dtos.CharacterDto
{
    public class CharacterResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int AttackBase { get; set; }
        public int DefenseBase { get; set; }
        public int HealthBase { get; set; }

        /// Navigation Properties for rarity
        public Guid RarityId { get; set; }
        public string RarityName { get; set; } = string.Empty;
        public int DropRate { get; set; }

        /// Navigation Properties for element type
        public Guid ElementTypeId { get; set; }
        public string ElementTypeName { get; set; } = string.Empty;
        public string ElementTypeImageUrl { get; set; } = string.Empty;
    }
}
