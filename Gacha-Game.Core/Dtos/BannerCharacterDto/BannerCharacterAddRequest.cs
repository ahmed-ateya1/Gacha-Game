namespace Gacha_Game.Core.Dtos.BannerCharacterDto
{
    public class BannerCharacterAddRequest
    {
        public Guid BannerId { get; set; }
        public List<Guid> CharacterIds { get; set; } = new();
    }
}
