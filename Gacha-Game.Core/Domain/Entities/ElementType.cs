namespace Gacha_Game.Core.Domain.Entities
{
    public class ElementType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? IconUrl { get; set; } = string.Empty;

        public virtual ICollection<Character> Characters { get; set; } = [];
    }
}
