# -*- coding: utf-8 -*-
import unittest

from gilded_rose import Item, GildedRose


class GildedRoseTest(unittest.TestCase):

    def update(self, item):
        GildedRose([item]).update_quality()
        return item

    # Normal items
    def test_normal_item_decreases_quality(self):
        item = self.update(Item("foo", 5, 10))
        self.assertEqual(4, item.sell_in)
        self.assertEqual(9, item.quality)

    def test_normal_item_degrades_twice_as_fast_after_sell_in(self):
        item = self.update(Item("foo", 0, 10))
        self.assertEqual(-1, item.sell_in)
        self.assertEqual(8, item.quality)

    def test_normal_item_quality_never_negative(self):
        item = self.update(Item("foo", 0, 0))
        self.assertEqual(0, item.quality)

    # Aged Brie
    def test_aged_brie_increases_quality(self):
        item = self.update(Item("Aged Brie", 2, 0))
        self.assertEqual(1, item.sell_in)
        self.assertEqual(1, item.quality)

    def test_aged_brie_increases_twice_after_sell_in(self):
        item = self.update(Item("Aged Brie", 0, 0))
        self.assertEqual(-1, item.sell_in)
        self.assertEqual(2, item.quality)

    def test_aged_brie_quality_never_exceeds_50(self):
        item = self.update(Item("Aged Brie", 2, 50))
        self.assertEqual(50, item.quality)

    # Sulfuras
    def test_sulfuras_never_changes(self):
        item = self.update(Item("Sulfuras, Hand of Ragnaros", 0, 80))
        self.assertEqual(0, item.sell_in)
        self.assertEqual(80, item.quality)

    def test_sulfuras_negative_sell_in_never_changes(self):
        item = self.update(Item("Sulfuras, Hand of Ragnaros", -1, 80))
        self.assertEqual(-1, item.sell_in)
        self.assertEqual(80, item.quality)

    # Backstage passes
    def test_backstage_pass_increases_by_1_more_than_10_days(self):
        item = self.update(Item("Backstage passes to a TAFKAL80ETC concert", 15, 20))
        self.assertEqual(14, item.sell_in)
        self.assertEqual(21, item.quality)

    def test_backstage_pass_increases_by_2_when_10_days_or_less(self):
        item = self.update(Item("Backstage passes to a TAFKAL80ETC concert", 10, 20))
        self.assertEqual(9, item.sell_in)
        self.assertEqual(22, item.quality)

    def test_backstage_pass_increases_by_3_when_5_days_or_less(self):
        item = self.update(Item("Backstage passes to a TAFKAL80ETC concert", 5, 20))
        self.assertEqual(4, item.sell_in)
        self.assertEqual(23, item.quality)

    def test_backstage_pass_quality_drops_to_zero_after_concert(self):
        item = self.update(Item("Backstage passes to a TAFKAL80ETC concert", 0, 20))
        self.assertEqual(-1, item.sell_in)
        self.assertEqual(0, item.quality)

    def test_backstage_pass_quality_never_exceeds_50(self):
        item = self.update(Item("Backstage passes to a TAFKAL80ETC concert", 5, 49))
        self.assertEqual(50, item.quality)

    # Conjured items
    def test_conjured_item_degrades_twice_as_fast(self):
        item = self.update(Item("Conjured Mana Cake", 3, 6))
        self.assertEqual(2, item.sell_in)
        self.assertEqual(4, item.quality)

    def test_conjured_item_degrades_four_times_after_sell_in(self):
        item = self.update(Item("Conjured Mana Cake", 0, 6))
        self.assertEqual(-1, item.sell_in)
        self.assertEqual(2, item.quality)

    def test_conjured_item_quality_never_negative(self):
        item = self.update(Item("Conjured Mana Cake", 0, 1))
        self.assertEqual(0, item.quality)


if __name__ == '__main__':
    unittest.main()
