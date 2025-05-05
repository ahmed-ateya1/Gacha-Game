namespace Gacha_Game.Core.Dtos.RarityDto
{
    public class RarityResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; } = 1;
        public string? ColorCode { get; set; } = string.Empty;
        public int DropRate { get; set; } = 0;
    }
}
