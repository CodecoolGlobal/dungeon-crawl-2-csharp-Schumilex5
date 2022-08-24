using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Items;

namespace Assets.Source.ExtensionMethods
{
    public static class Inventory
    {
        public static string GetInventory(this List<Item> inventory)
        {
            string items = "";
            foreach (Item item in inventory)
            {
                items += $"{item.DefaultName}: {item.GetDurability()}\n";
            }
            return items;
        }

        public static Item GetItemFromInventory(this List<Item> _inventory, string className)
        {
            foreach (var item in _inventory)
            {
                if (item.DefaultName == $"{className}") return item;
            }
            return null;
        }

                
    }
}
