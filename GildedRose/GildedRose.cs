using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    private static void AdjustQuality(Item item, int delta) =>
        item.Quality = Math.Clamp(item.Quality + delta, 0, 50);

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            if (item.Name == "Sulfuras, Hand of Ragnaros") continue;
            item.SellIn--;
            switch (item.Name)
            {
                case "Aged Brie":
                    if (item.SellIn < 0) AdjustQuality(item, +2);
                    else AdjustQuality(item, +1);
                    break;

                case "Backstage passes to a TAFKAL80ETC concert":
                    if (item.SellIn < 0) item.Quality = 0;
                    else if (item.SellIn < 5) AdjustQuality(item, +3);
                    else if (item.SellIn < 10) AdjustQuality(item, +2);
                    else AdjustQuality(item, +1);
                    break;

                default:
                    bool isConjured = item.Name.StartsWith("Conjured");
                    int degradeRate = isConjured ? 2 : 1;
                    if (item.SellIn < 0) AdjustQuality(item, -2 * degradeRate);
                    else AdjustQuality(item, -1 * degradeRate);
                    break;
            }
        }
    }
}