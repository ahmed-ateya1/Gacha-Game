namespace Gacha_Game.Core.Domain.Entities
{
    public class Rarity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ColorCode { get; set; } = string.Empty;
        public int DropRate { get; set; }
        public virtual ICollection<Character> Characters { get; set; } = [];
    }
}
