using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Test]
    public void TestingItemsAgedBrie()
    {
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 } };
        var gildedRose = new GildedRose(items);
        
        // Um dia depois
        gildedRose.UpdateQuality();
        Assert.That(items[0].Name, Is.EqualTo("Aged Brie"));  // O nome não muda após UpdateQuality
        Assert.That(items[0].SellIn, Is.EqualTo(9));  // SellIn diminui em 1
        Assert.That(items[0].Quality, Is.EqualTo(21));  // Quality aumenta em 2
    }
    
    [Test]
    public void TestConjuredItemsDegrade()
    {
        // Os itens conjurados perdem qualidade duas vezes mais rápido
        var items = new List<Item> { 
            new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 },
            new Item { Name = "Elixir of the Mongoose", SellIn = 3, Quality = 6 }  // Item normal para comparação
        };
        
        var gildedRose = new GildedRose(items);
        
        gildedRose.UpdateQuality();
        
        // Itens conjurados devem perder 2 pontos de qualidade
        Assert.That(items[0].Name, Is.EqualTo("Conjured Mana Cake"));
        Assert.That(items[0].SellIn, Is.EqualTo(2));
        Assert.That(items[0].Quality, Is.EqualTo(4));
        
        // Itens normais devem perder 1 ponto de qualidade
        Assert.That(items[1].Name, Is.EqualTo("Elixir of the Mongoose"));
        Assert.That(items[1].SellIn, Is.EqualTo(2));
        Assert.That(items[1].Quality, Is.EqualTo(5));
    }
    
    [Test]
    public void TestConjuredItemsAfterExpiry()
    {
        // Os itens conjurados perdem qualidade duas vezes mais rápido após o vencimento
        var items = new List<Item> { 
            new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 10 },
            new Item { Name = "Elixir of the Mongoose", SellIn = 0, Quality = 10 }  // Item normal para comparação
        };
        
        var gildedRose = new GildedRose(items);
        
        gildedRose.UpdateQuality();
        
        Assert.That(items[0].Quality, Is.EqualTo(6)); // Itens conjurados devem perder 4 pontos de qualidade (2x para itens expirados)
        Assert.That(items[1].Quality, Is.EqualTo(8)); // Itens normais devem perder 2 pontos de qualidade
    }
    
    [Test]
    public void TestBackstagePassesQualityIncreasesNormally()
    {
        // Caso: Backstage passes aumenta qualidade em 1 quando SellIn > 10
        var items = new List<Item> { 
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 }
        };
        
        var gildedRose = new GildedRose(items);
        
        gildedRose.UpdateQuality();
        
        Assert.That(items[0].Name, Is.EqualTo("Backstage passes to a TAFKAL80ETC concert"));
        Assert.That(items[0].SellIn, Is.EqualTo(14));
        Assert.That(items[0].Quality, Is.EqualTo(21)); // Qualidade aumenta em 1
    }
    
    [Test]
    public void TestBackstagePassesQualityIncreasesByTwo()
    {
        // Caso: Backstage passes aumenta qualidade em 2 quando 5 <= SellIn <= 10
        var items = new List<Item> { 
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 }
        };
        
        var gildedRose = new GildedRose(items);
        
        gildedRose.UpdateQuality();
        
        Assert.That(items[0].Name, Is.EqualTo("Backstage passes to a TAFKAL80ETC concert"));
        Assert.That(items[0].SellIn, Is.EqualTo(9));
        Assert.That(items[0].Quality, Is.EqualTo(22)); // Qualidade aumenta em 2
    }
    
    [Test]
    public void TestBackstagePassesQualityIncreasesByThree()
    {
        // Caso: Backstage passes aumenta qualidade em 3 quando 5 >= SellIn > 0
        var items = new List<Item> { 
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 }
        };
        
        var gildedRose = new GildedRose(items);
        
        // Depois de um dia
        gildedRose.UpdateQuality();
        
        Assert.That(items[0].Name, Is.EqualTo("Backstage passes to a TAFKAL80ETC concert"));
        Assert.That(items[0].SellIn, Is.EqualTo(4));
        Assert.That(items[0].Quality, Is.EqualTo(23)); // Qualidade aumenta em 3
    }
    
    [Test]
    public void TestBackstagePassesQualityDropsToZeroAfterEvent()
    {
        // Caso: Backstage passes perde todo o valor depois do evento (SellIn < 0)
        var items = new List<Item> { 
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 }
        };
        
        var gildedRose = new GildedRose(items);
        
        // Depois de um dia (após o evento)
        gildedRose.UpdateQuality();
        
        Assert.That(items[0].Name, Is.EqualTo("Backstage passes to a TAFKAL80ETC concert"));
        Assert.That(items[0].SellIn, Is.EqualTo(-1));
        Assert.That(items[0].Quality, Is.EqualTo(0)); // Qualidade cai para 0 após o evento
    }
}