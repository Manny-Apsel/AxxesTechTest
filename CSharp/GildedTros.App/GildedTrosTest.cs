using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        /// <summary>
        /// General test that everything works
        /// </summary>
        [Fact]
        public void GeneralRunTest()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal("foo", Items[0].Name);
        }

        /// <summary>
        /// Quality decreases by 1 after one day
        /// </summary>
        [Fact]
        public void QualityDecreaseAfterOneDay()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 1, Quality = 1 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(0, Items.First().Quality);
        }

        /// <summary>
        /// SellIn decreases by 1 after one day
        /// </summary>
        [Fact]
        public void SellInDecreaseAfterOneDay()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 1, Quality = 1 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(0, Items.First().SellIn);
        }

        /// <summary>
        /// Quality decreases twice as fast once sell date has passed
        /// </summary>
        [Fact]
        public void DoubleQualityDecreaseAfterSellData()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 6 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(4, Items.First().Quality);
        }

        /// <summary>
        /// Quality is never negative after one day
        /// </summary>
        [Fact]
        public void Quality()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 2, Quality = 0 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(0, Items.First().Quality);
        }

        /// <summary>
        /// Quality of "Good Wine" increases by 1 after one day
        /// </summary>
        [Fact]
        public void QualityIncreaseGoodWine()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 2, Quality = 5 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(6, Items.First().Quality);
        }

        /// <summary>
        /// Quality of "Good Wine" increases by 1 after one day when quality starts at 0
        /// </summary>
        [Fact]
        public void QualityIncreaseGoodWineStartQualityZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 2, Quality = 0 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(1, Items.First().Quality);
        }

        /// <summary>
        /// Quality never increases higher than 50
        /// </summary>
        [Fact]
        public void QualityIncreaseStop()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 2, Quality = 50 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(50, Items.First().Quality);
        }

        /// <summary>
        /// "B-DAWG Keychain", being a legendary item, never has to be sold or decreases in Quality
        /// </summary>
        [Fact]
        public void KeychainHasNoChanges()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "B-DAWG Keychain", SellIn = 40, Quality = 80 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(39, Items.First().SellIn);
            Assert.Equal(80, Items.First().Quality);
        }

        /// <summary>
        /// Backstage passes quality increases by 1 when there are more than 10 days left
        /// </summary>
        [Fact]
        public void BackstagePassesIncreaseInQualityByOne()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 20, Quality = 20 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(21, Items.First().Quality);
        }
        
        
        /// <summary>
        /// Backstage passes quality increases by 2 when there are 10 days or less
        /// </summary>
        [Fact]
        public void BackstagePassesIncreaseInQualityByTwo()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 20 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(22, Items.First().Quality);
        }

        /// <summary>
        /// Backstage passes quality increases by 3 when there are 5 days or less
        /// </summary>
        [Fact]
        public void BackstagePassesIncreaseInQualityByThree()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 5, Quality = 20 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(23, Items.First().Quality);
        }

        /// <summary>
        /// Backstage passes quality drops to 0 after the concert
        /// </summary>
        [Fact]
        public void BackstagePassesQualityDecreaseToNull()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 0, Quality = 20 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(0, Items.First().Quality);
        }

        /// <summary>
        /// Backstage passes quality can't pass limit of 50
        /// </summary>
        [Fact]
        public void BackstagePassesQualityIncreaseLimit()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 4, Quality = 49 } };
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(50, Items.First().Quality);
        }

        /// <summary>
        /// Smelly items decrease in quality twice as fast
        /// </summary>
        [Fact]
        public void SmellyItemsDecrease()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Duplicate Code", SellIn = 5, Quality = 20 }};
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(18, Items.First().Quality);
        }
        
        /// <summary>
        /// Smelly items decrease in quality twice as fast
        /// </summary>
        [Fact]
        public void SmellyItemsDecreaseAfterSellInValueReached()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Long Methods", SellIn = 0, Quality = 20 }};
            GildedTros app = new GildedTros(Items);

            app.UpdateQuality();

            Assert.Equal(16, Items.First().Quality);
        }
        
    }
}