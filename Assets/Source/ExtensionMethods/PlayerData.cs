﻿using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;

namespace Assets.Source.ExtensionMethods
{
    [System.Serializable]
    public class PlayerData
    {
        public int health;
        public int level;
        public int score;
        public int[] position;

        public PlayerData(Player player)
        {
            health = player.GetHealth();
            score = player.GetScore();
            level = player.GetLevel();

            position = new int[2];
            position[0] = player.Position.x;
            position[1] = player.Position.y;
        }
        
    }
}