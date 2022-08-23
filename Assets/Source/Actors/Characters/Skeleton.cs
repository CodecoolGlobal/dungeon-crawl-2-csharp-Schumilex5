using DungeonCrawl.Core;
using UnityEngine;
using System;
using Assets.Source.ExtensionMethods;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        private int damage;
        public new int Health { get; }
        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";

        public Skeleton()
        {
            damage = 2;
            this.Health = 10;
        }

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            if (anotherActor.Position == targetPosition)
            {
                if(anotherActor.GetType() == typeof(Player))
                {
                    ((Player)anotherActor).AttackSkeleton(this);
                }
                return false;
            }
            return true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            this.HomingOnPlayer();
        }


        protected override void OnDeath()
        {
            Player player = FindObjectOfType<Player>();
            player.SetScore(100);
            Debug.Log("Well, I was already dead anyway...");
        }

        public int GetDamage()
        {
            return damage;
        }
        
        public int GetHealth()
        {
            return this.Health;
        }

    }
}
