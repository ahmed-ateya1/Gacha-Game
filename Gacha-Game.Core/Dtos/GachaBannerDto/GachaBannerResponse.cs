namespace Gacha_Game.Core.Dtos.GachaBannerDto
{
    public class GachaBannerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int CostPerPull { get; set; }
        public int PullsForGuaranteedRarity { get; set; }

        public string BannerImageUrl { get; set; } = string.Empty;
    }
}
