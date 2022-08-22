using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        private int damage;
        public new int Health { get; }

        public Skeleton()
        {
            damage = 2;
            this.Health = 10;
        }

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            return true;
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

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}
