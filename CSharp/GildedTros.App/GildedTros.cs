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


            }
        }

        private bool IsQualityInAcceptableRange(Item item) => item.Quality > 0 && item.Quality < 50;
        
        private bool IsLegendary(Item item) => item.Name == "B-DAWG Keychain";
    }
}
