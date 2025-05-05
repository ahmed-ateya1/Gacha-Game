namespace Gacha_Game.Core.Domain.Entities
{
    public class GachaBanners
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CostPerPull { get; set; }
        public int PullsForGuaranteedRarity { get; set; }
        public string BannerImageUrl { get; set; } = string.Empty;

        public virtual ICollection<BannerCharacter> BannerCharacters { get; set; } = [];
        public virtual ICollection<GachaPulls> GachaPulls { get; set; } = [];

        public bool IsActive => StartDate <= DateTime.UtcNow && EndDate >= DateTime.UtcNow;
    }
}
