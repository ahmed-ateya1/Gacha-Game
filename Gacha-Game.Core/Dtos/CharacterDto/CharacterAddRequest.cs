using Microsoft.AspNetCore.Http;

namespace Gacha_Game.Core.Dtos.CharacterDto
{
    public class CharacterAddRequest
    {
        public string Name { get; set; } 
        public string? Description { get; set; }
        public IFormFile Image { get; set; } 
        public int AttackBase { get; set; }
        public int DefenseBase { get; set; }
        public int HealthBase { get; set; }
        public Guid RarityId { get; set; }
        public Guid ElementTypeId { get; set; }
    }
}
