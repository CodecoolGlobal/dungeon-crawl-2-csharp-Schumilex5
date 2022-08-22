using System.Collections.Generic;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Actors.Items
{
    public abstract class Item : Actor
    {
        private int _durability;
        private int _bonusDamage;
        private int _useCost = -1;

        public Item(int durability, int bonusDamage)
        {
            _durability = durability;
            _bonusDamage = bonusDamage;
        }
        
        public Item(int durability, int bonusDamage, int useCost)
        {
            _durability = durability;
            _bonusDamage = bonusDamage;
            _useCost = useCost;
        }
        
        public int GetDurability()
        {
            return _durability;
        }
        public int GetBonusDmg()
        {
            return _bonusDamage;
        }

        public void ChangeDurability(List<Item> inventory)
        {
            _durability -= _useCost;
            ManageInventory(inventory);
        }
        
        public void ChangeDurability(List<Item> inventory, int value)
        {
            _durability += value;
            ManageInventory(inventory);
        }

        public int GetUseCost()
        {
            return _useCost;
        }

        private void ManageInventory(List<Item> inventory)
        {
            Player player = FindObjectOfType<Player>();
            foreach (var item in inventory)
            {
                if (item.GetDurability() < 1)
                {
                    inventory.Remove(item);
                    player.ShowStats();
                }
            }
        }
    }
}