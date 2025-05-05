using Gacha_Game.Core.Helper;

namespace Gacha_Game.Core.Dtos.SpinDto
{
    public class SpinAddRequest
    {
        public Guid BannerID { get; set; }
        public PullTypes PullType { get; set; }
    }
}
