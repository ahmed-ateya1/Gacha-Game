using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Dtos.BannerCharacterDto;
using Gacha_Game.Core.Dtos.CharacterDto;
using Gacha_Game.Core.Dtos.GachaBannerDto;
using Gacha_Game.Core.Dtos.InventoryDto;
using Mapster;

namespace Gacha_Game.Core.MapsterConfiguration
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<Character, CharacterResponse>
                .NewConfig()
                .Map(dest => dest.RarityName, src => src.Rarity != null ? src.Rarity.Name : string.Empty)
                .Map(dest => dest.DropRate, src => src.Rarity != null ? src.Rarity.DropRate : 0)
                .Map(dest => dest.ElementTypeName, src => src.ElementType != null ? src.ElementType.Name : string.Empty)
                .Map(dest => dest.ElementTypeImageUrl, src => src.ElementType != null ? src.ElementType.IconUrl : string.Empty);

            TypeAdapterConfig<GachaBanners, GachaBannerResponse>
                .NewConfig()
                .Map(dest => dest.IsActive, src => src.IsActive);

            TypeAdapterConfig<Inventory, InventoryResponse>
                .NewConfig()
                .Map(dest => dest.CharacterName, src => src.Character != null ? src.Character.Name : string.Empty)
                .Map(dest => dest.CharacterRarity, src => src.Character != null && src.Character.Rarity != null ? src.Character.Rarity.Name : string.Empty)
                .Map(dest => dest.CharacterElement, src => src.Character != null && src.Character.ElementType != null ? src.Character.ElementType.Name : string.Empty)
                .Map(dest => dest.CharacterImageUrl, src => src.Character != null && src.Character.ImageUrl != null ? src.Character.ImageUrl : string.Empty);

            TypeAdapterConfig<BannerCharacter, BannerCharaterResponse>
                .NewConfig()
                .Map(dest => dest.BannerId, src => src.BannerId)
                .Map(dest => dest.CharacterResponses, src => new List<CharacterResponse>
                {
                    src.Character != null ? src.Character.Adapt<CharacterResponse>() : null
                }.Where(c => c != null).ToList());

        }
    }
}
