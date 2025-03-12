using System;
using System.Collections.Generic;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> Items;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                item.SellIn -= 1;

                if (IsLegendary(item))
                {
                    item.Quality = 80;
                    continue;
                }

                if (!IsQualityInAcceptableRange(item))
                {
                    continue;
                }

                if (IsWine(item))
                {
                    item.Quality += 1;
                    continue;
                }

                if (IsBackstagePass(item))
                {
                    if (item.SellIn < 0)
                    {
                        item.Quality = 0;
                        continue;
                    }

                    item.Quality += EvaluateQualityChangeForBackStagePass(item.SellIn);

                    continue;
                }
            }
        }

        private int EvaluateQualityChangeForBackStagePass(int sellIn)
        {
            if (sellIn < 5)
            {
                return 3;
            }
            else if (sellIn < 10)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        private bool IsBackstagePass(Item item) => item.Name.Contains("Backstage passes");

        private bool IsWine(Item item) => item.Name == "Good Wine";

        private bool IsQualityInAcceptableRange(Item item) => item.Quality > 0 && item.Quality < 50;

        private bool IsLegendary(Item item) => item.Name == "B-DAWG Keychain";
    }
}
