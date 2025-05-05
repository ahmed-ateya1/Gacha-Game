using Microsoft.AspNetCore.Http;

namespace Gacha_Game.Core.Dtos.GachaBannerDto
{
    public class GachaBannerUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CostPerPull { get; set; }
        public int PullsForGuaranteedRarity { get; set; }
        public IFormFile? BannerImage { get; set; } 
    }
}
