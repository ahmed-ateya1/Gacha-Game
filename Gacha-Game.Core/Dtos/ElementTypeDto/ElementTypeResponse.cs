﻿namespace Gacha_Game.Core.Dtos.ElementTypeDto
{
    public record ElementTypeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? IconUrl { get; set; } = string.Empty;
    }
}
