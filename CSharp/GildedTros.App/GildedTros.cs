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

                // determine quality decrease
                switch ((IsSellInPositive(item.SellIn), IsItemSmelly(item.Name)))
                {
                    case (true, false):
                        item.Quality -= 1;
                        break;
                    case (false, false):
                    case (true, true):
                        item.Quality -= 2;
                        break;
                    case (false, true):
                        item.Quality -= 4;
                        break;
                }
            }
        }
        private bool IsItemSmelly(string name) => name == "Duplicate Code" || name == "Long Methods" || name == "Ugly Variable Names";
        private bool IsSellInPositive(int sellIn) => sellIn >= 0;

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
