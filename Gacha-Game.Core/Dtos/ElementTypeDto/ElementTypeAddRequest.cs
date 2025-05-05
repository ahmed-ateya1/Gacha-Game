using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Gacha_Game.Core.Dtos.ElementTypeDto
{
    public class ElementTypeAddRequest
    {
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(50, ErrorMessage = "Name must be less than 50 characters!")]
        public string Name { get; init; } 

        public IFormFile? Icon { get; init; }
    }
}
