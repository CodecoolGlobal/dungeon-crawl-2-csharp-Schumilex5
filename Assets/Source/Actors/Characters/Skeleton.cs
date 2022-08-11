using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public static readonly int damage = 2;
        public new int Health { get; private set; } = 2;

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            return true;
        }

        protected override void OnDeath()
        {
            Player.SetScore(100);
            Debug.Log("Well, I was already dead anyway...");
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}
