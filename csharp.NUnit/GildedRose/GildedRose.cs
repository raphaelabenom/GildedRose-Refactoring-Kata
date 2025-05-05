using System.Collections.Generic;

namespace GildedRoseKata;

// Objetivos:
// - Refatoração da classe GildedRose para encapsular a lógica de atualização da qualidade do item em um método separado.
// - Aprimoramento das regras de atualização da qualidade para itens como "Aged Brie", "Backstage passes" e "Conjured".
// - Adição de testes unitários incluindo "Aged Brie", "Conjured" e "Backstage passes".

public class GildedRose
{
    private IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
      UpdateQuality(Items[i]);
        }
    }

    private static void UpdateQuality(Item item) {
            if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                // Se não for Aged Brie ou Backstage passes então executar essa condição
                // Diminui a qualidade em 1 para itens normais
                // Diminui a qualidade em 2 para itens Conjured
                // Não diminui a qualidade para Sulfuras
                if (item.Quality > 0 && item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    // Diminui a qualidade em 1 para itens normais
                    item.Quality--;
                    // Para itens Conjured, diminui mais 1 (total de 2)
                    if (item.Name.Contains("Conjured") && item.Quality > 0)
                    {
                        item.Quality--;
                    }
                }
            }
            else
            {
                if (item.Quality < 50)
                {
                    // Aumenta a qualidade em 1 para Aged Brie e Backstage passes
                    item.Quality++;

                    if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        // Aged Brie e Backstage
                        // A qualidade (quality) aumenta em 2 unidades quando a data de venda (SellIn) é igual ou menor que 10.
                        // A qualidade (quality) aumenta em 3 unidades quando a data de venda (SellIn) é igual ou menor que 5.
                        // A qualidade (quality) do item vai direto à 0 quando a data de venda (SellIn) tiver passado.
                        if (item.SellIn < 11)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality++;
                            }
                        }

                        if (item.SellIn < 6)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality++;
                            }
                        }
                    }
                }
            }

            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn--;
            }
            // Antes da data de venda expirar
            if (item.SellIn < 0)
            {
                if (item.Name != "Aged Brie")
                {
                    if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        // Depois da data de venda expirar
                        if (item.Quality > 0)
                        {
                            if (item.Name != "Sulfuras, Hand of Ragnaros")
                            {
                                // Diminui a qualidade em 1 para itens normais após a data de venda
                                item.Quality--;
                                // Para itens Conjured, diminui mais 1 (total de 2) após a data de venda
                                if (item.Name.Contains("Conjured") && item.Quality > 0)
                                {
                                    item.Quality--;
                                }
                            }
                        }
                    }
                    else
                    {
                        item.Quality -= item.Quality;
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality++;
                    }
                }
            }
        }
}
