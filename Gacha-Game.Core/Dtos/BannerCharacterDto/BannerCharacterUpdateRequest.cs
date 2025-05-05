namespace Gacha_Game.Core.Dtos.BannerCharacterDto
{
    public class BannerCharacterUpdateRequest
    {
        public Guid BannerId { get; set; }
        public List<Guid> CharacterIds { get; set; } = new();
    }
}
