namespace Gacha_Game.Core.Dtos.RarityDto
{
    public class RarityUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string? ColorCode { get; set; } 
        public int DropRate { get; set; } = 0;
    }
}
