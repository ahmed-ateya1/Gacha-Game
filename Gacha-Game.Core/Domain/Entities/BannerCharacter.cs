namespace Gacha_Game.Core.Domain.Entities
{
    public class BannerCharacter
    {
        public Guid Id { get; set; }
        public Guid BannerId { get; set; }
        public virtual GachaBanners Banner { get; set; } = default!;
        public Guid CharacterId { get; set; }
        public virtual Character Character { get; set; } = default!;

    }
}
