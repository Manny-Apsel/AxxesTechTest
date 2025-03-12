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

                    if (item.SellIn < 5)
                    {
                        item.Quality += 3;
                    }
                    else if (item.SellIn < 10)
                    {
                        item.Quality += 2;
                    }
                    else
                    {
                        item.Quality += 1;
                    }
                    continue;
                }
            }
        }

        private bool IsBackstagePass(Item item) => item.Name.Contains("Backstage passes");

        private bool IsWine(Item item) => item.Name == "Good Wine";

        private bool IsQualityInAcceptableRange(Item item) => item.Quality > 0 && item.Quality < 50;

        private bool IsLegendary(Item item) => item.Name == "B-DAWG Keychain";
    }
}
