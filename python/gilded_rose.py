# -*- coding: utf-8 -*-

AGED_BRIE = "Aged Brie"
SULFURAS = "Sulfuras, Hand of Ragnaros"
BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert"
CONJURED = "Conjured"
MAX_QUALITY = 50
MIN_QUALITY = 0


class GildedRose(object):

    def __init__(self, items):
        self.items = items

    def update_quality(self):
        for item in self.items:
            self._update_item(item)

    def _update_item(self, item):
        if item.name == SULFURAS:
            return

        if item.name == AGED_BRIE:
            self._increase_quality(item, 1)
        elif item.name == BACKSTAGE_PASSES:
            self._update_backstage_pass_quality(item)
        elif item.name.startswith(CONJURED):
            self._decrease_quality(item, 2)
        else:
            self._decrease_quality(item, 1)

        item.sell_in -= 1

        if item.sell_in < 0:
            self._apply_expired_quality(item)

    def _update_backstage_pass_quality(self, item):
        if item.sell_in <= 5:
            self._increase_quality(item, 3)
        elif item.sell_in <= 10:
            self._increase_quality(item, 2)
        else:
            self._increase_quality(item, 1)

    def _apply_expired_quality(self, item):
        if item.name == AGED_BRIE:
            self._increase_quality(item, 1)
        elif item.name == BACKSTAGE_PASSES:
            item.quality = MIN_QUALITY
        elif item.name.startswith(CONJURED):
            self._decrease_quality(item, 2)
        else:
            self._decrease_quality(item, 1)

    def _increase_quality(self, item, amount):
        item.quality = min(item.quality + amount, MAX_QUALITY)

    def _decrease_quality(self, item, amount):
        item.quality = max(item.quality - amount, MIN_QUALITY)


class Item:
    def __init__(self, name, sell_in, quality):
        self.name = name
        self.sell_in = sell_in
        self.quality = quality

    def __repr__(self):
        return "%s, %s, %s" % (self.name, self.sell_in, self.quality)
