using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> items;
        string[] smellyItems;
        public GildedTros(IList<Item> Items)
        {
            this.items = Items;
            smellyItems = JsonLoader.LoadJsonArray("smellyItems.json");
        }

        public void UpdateQuality()
        {
            foreach (var item in items)
            {
                item.SellIn -= 1;

                if (IsLegendary(item))
                {
                    item.Quality = 80;
                    continue;
                }

                if (IsQualityAboveMaximum(item.Quality))
                {
                    continue;
                }

                if (IsWine(item))
                {
                    item.Quality += 1;
                    continue;
                }

                if (IsQualityUnderMinimum(item.Quality))
                {
                    continue;
                }

                if (IsBackstagePass(item))
                {
                    HandleQualityChangeBackStagePass(item);
                    continue;
                }

                DetermineQualityDecrease(item);
            }
        }

        private void HandleQualityChangeBackStagePass(Item item)
        {
            if (item.SellIn < 0)
            {
                item.Quality = 0;
                return;
            }

            var qualityIncrease = EvaluateQualityChangeForBackStagePass(item.SellIn);

            item.Quality += qualityIncrease;

            if (IsQualityAboveMaximum(item.Quality))
            {
                item.Quality = 50;
            }
        }

        private void DetermineQualityDecrease(Item item)
        {
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

        private bool IsItemSmelly(string name) => smellyItems.Contains(name);
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

        private bool IsQualityUnderMinimum(int quality) => quality <= 0;
        private bool IsQualityAboveMaximum(int quality) => quality >= 50;

        private bool IsLegendary(Item item) => item.Name == "B-DAWG Keychain";
    }
}
