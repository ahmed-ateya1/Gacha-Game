using System.ComponentModel.DataAnnotations;

namespace Gacha_Game.Core.Dtos.InventoryDto
{
    public class InventoryAddRequest
    {
        [Required(ErrorMessage ="CharacterID is required!")]
        public Guid CharacterID { get; set; }
    }
}
