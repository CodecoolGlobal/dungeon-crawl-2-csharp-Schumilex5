using UnityEngine;
using Assets.Source.Actors;
using Assets.Source.ExtensionMethods;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Enemy
    {
        public new int Health { get; }
        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";

        public Skeleton()
        {
            Damage = 2;
            Health = 10;
        }

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            if (anotherActor.Position == targetPosition)
            {
                if (anotherActor.GetType() == typeof(Player))
                {
                    //this.ApplyDmgOnPlayerAndEnemy((Player)anotherActor);
                    return false;
                }
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

    }
}