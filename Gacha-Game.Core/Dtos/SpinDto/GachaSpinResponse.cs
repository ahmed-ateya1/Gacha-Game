using Gacha_Game.Core.Dtos.CharacterDto;
using Gacha_Game.Core.Helper;

namespace Gacha_Game.Core.Dtos.SpinDto
{
    public class GachaSpinResponse
    {
        public Guid BannerId { get; set; }
        public PullTypes PullType { get; set; }
        public List<CharacterResponse> Characters { get; set; } = [];
    }
}
