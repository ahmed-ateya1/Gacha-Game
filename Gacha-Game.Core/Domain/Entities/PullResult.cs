namespace Gacha_Game.Core.Domain.Entities
{
    public class PullResult
    {
        public Guid Id { get; set; }
        public Guid PullId { get; set; }
        public virtual GachaPulls Pull { get; set; } = default!;
        public Guid InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; } = default!;
    }
}
