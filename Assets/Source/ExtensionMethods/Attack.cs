using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;

namespace Assets.Source.Actors.ExtensionMethods
{
    public static class PlayerAttack
    {
        public static void AttackFromPlayer(this Actor enemy, Player player)
        {
            if (player.timeBtwAttack <= 0)
            {
                //then you can attack
                player.timeBtwAttack = Player.startTimeBtwAttack;
            }
            else
            {
                player.timeBtwAttack -= Time.deltaTime;
            }

            ((Skeleton)enemy).ApplyDamage(Player.damage);
            player.ApplyDamage(Skeleton.damage);
        }
    }
}
