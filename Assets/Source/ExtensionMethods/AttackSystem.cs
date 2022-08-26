using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Actors;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using UnityEngine;

namespace Assets.Source.ExtensionMethods
{
    public static class ApplyDamage
    {
        public static void ApplyDmgOnPlayerAndEnemy(this Player player, Enemy enemy)
        {
            player.Health -= enemy.Damage;
            enemy.Health -= player.Damage;

            if (player.Health < 0) player.KillPlayer();
            if(enemy.Health <0 ) enemy.KillEnemy();
        }

        public static void ApplyDmgOnSelf(this Character character, int damage)
        {
            character.Health -= damage;
            if (character.GetType() == typeof(Enemy) && character.Health < 0) ((Enemy)character).KillEnemy();
            if (character.GetType() == typeof(Player) && character.Health < 0) ((Player)character).KillPlayer();
        }

        public static void HealSelf(this Character character, int health)
        {
            character.Health += health;
        }
    }

}