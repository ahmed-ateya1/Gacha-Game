using Gacha_Game.Core.Domain.Entities;

namespace Gacha_Game.Core.Helper
{
    public static class BuildWeightedPoolHelper
    {
        public static IEnumerable<Character> BuildWeightedPool(GachaBanners banner)
        {
            var pool = new List<Character>();
            foreach (var bc in banner.BannerCharacters)
            {
                int weight = bc.Character.Rarity.DropRate;
                pool.AddRange(Enumerable.Repeat(bc.Character, weight));
            }
            return pool;
        }
    }
}
