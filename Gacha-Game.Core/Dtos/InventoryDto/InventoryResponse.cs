namespace Gacha_Game.Core.Dtos.InventoryDto
{
    public class InventoryResponse
    {
        public Guid Id { get; set; }
        public DateTime ObtainedDate { get; set; }
        public Guid UserID { get; set; }
        public Guid CharacterID { get; set; }
        public string CharacterName { get; set; } = default!;
        public string CharacterImageUrl { get; set; } = default!;
        public string CharacterRarity { get; set; } = default!;
        public string CharacterElement { get; set; } = default!;
    }
}
