using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{

    private Item ItemCreator(string name, int sellIn, int quality)
    {
        return new Item { Name = name, SellIn = sellIn, Quality = quality };
    }

    [Test]
    public void StandardItem_QualityDegradesBy1BeforeSellDate()
    {
        var items = new List<Item> { ItemCreator("Apple", 10, 20) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(9));
        Assert.That(items[0].Quality, Is.EqualTo(19));
    }

    [Test]
    public void StandardItem_QualityDegradesBy2AfterSellDate()
    {
        var items = new List<Item> { ItemCreator("Apple", 0, 20) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(-1));
        Assert.That(items[0].Quality, Is.EqualTo(18));
    }

    [Test]
    public void StandardItem_MinimumZero()
    {
        var items = new List<Item> { ItemCreator("Apple", 5, 0) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(4));
        Assert.That(items[0].Quality, Is.EqualTo(0));
    }

    [Test]
    public void AgedBrie_QualityIncreasesOverTime()
    {
        var items = new List<Item> { ItemCreator("Aged Brie", 5, 0) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(4));
        Assert.That(items[0].Quality, Is.EqualTo(1));
    }

    [Test]
    public void AgedBrie_QualityIncreasesByTwoPastSellIn()
    {
        var items = new List<Item> { ItemCreator("Aged Brie", 0, 25) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(-1));
        Assert.That(items[0].Quality, Is.EqualTo(27));
    }

    [Test]
    public void AgedBrie_QualityMaxesAt50()
    {
        var items = new List<Item> { ItemCreator("Aged Brie", 5, 50) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(4));
        Assert.That(items[0].Quality, Is.EqualTo(50));
    }


    [Test]
    public void Sulfuras_QualityAlways80()
    {
        var items = new List<Item> { ItemCreator("Sulfuras, Hand of Ragnaros", 5, 80) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(5));
        Assert.That(items[0].Quality, Is.EqualTo(80));
    }

    [Test]
    public void BackstagePasses_QualityIncreasesByOneIfAbove10DaysSellIn()
    {
        var items = new List<Item> { ItemCreator("Backstage passes to a TAFKAL80ETC concert", 12, 20) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(11));
        Assert.That(items[0].Quality, Is.EqualTo(21));
    }

    [Test]
    public void BackstagePasses_QualityIncreasesByTwoIfBelowTenAndAboveFiveDaysSellIn()
    {
        var items = new List<Item> { ItemCreator("Backstage passes to a TAFKAL80ETC concert", 7, 20) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(6));
        Assert.That(items[0].Quality, Is.EqualTo(22));
    }

    [Test]
    public void BackstagePasses_QualityIncreasesByThreeIfBelowFiveDaysSellIn()
    {
        var items = new List<Item> { ItemCreator("Backstage passes to a TAFKAL80ETC concert", 4, 20) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(3));
        Assert.That(items[0].Quality, Is.EqualTo(23));
    }

    [Test]
    public void BackstagePasses_QualityZeroAfterConcert()
    {
        var items = new List<Item> { ItemCreator("Backstage passes to a TAFKAL80ETC concert", 0, 20) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(-1));
        Assert.That(items[0].Quality, Is.EqualTo(0));
    }
}