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
    }
}