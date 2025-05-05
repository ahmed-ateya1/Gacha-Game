using Gacha_Game.Core.Dtos.CharacterDto;

namespace Gacha_Game.Core.Dtos.BannerCharacterDto
{
    public class BannerCharaterResponse
    {
        public Guid BannerId { get; set; }
        public List<CharacterResponse> CharacterResponses { get; set; } = new();
    }
}
