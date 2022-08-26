using System.Collections.Generic;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Items;

namespace Assets.Source.ExtensionMethods
{
    // [System.Serializable]
    // public class PlayerData
    // {
    //     public int health;
    //     public int level;
    //     public int score;
    //     public int[] position;
    //
    //     public PlayerData(Player player)
    //     {
    //         health = player.Health;
    //         score = player.GetScore();
    //
    //         position = new int[2];
    //         position[0] = player.Position.x;
    //         position[1] = player.Position.y;
    //     }
    //     
    // }
    
    [System.Serializable]
    public class PlayerData
    {
        public int health;
        public int score;
        public int[] position;
        public string[] Items;
        public int[] Durabilities;
        public int playerSpriteId;
        

        public PlayerData(Player player)
        {
            health = player.Health;
            score = player.GetScore();
            Items = GetItemNames(player);
            Durabilities = GetItemDurabilities(player);

            position = new int[2];
            position[0] = player.Position.x;
            position[1] = player.Position.y;
        }

        private string[] GetItemNames(Player player)
        {
            List<Item> inventory = player.GetInventory();
            string[] itemNames = new string[inventory.Count];
            for (int itemIndex = 0; itemIndex < inventory.Count; itemIndex++)
            {
                itemNames[itemIndex] = inventory[itemIndex].DefaultName;
            }

            return itemNames;
        }
        
        private int[] GetItemDurabilities(Player player)
        {
            List<Item> inventory = player.GetInventory();
            int[] itemDurabilities = new int[inventory.Count];
            for (int itemIndex = 0; itemIndex < inventory.Count; itemIndex++)
            {
                itemDurabilities[itemIndex] = inventory[itemIndex].GetDurability();
            }

            return itemDurabilities;
        }
    }
}