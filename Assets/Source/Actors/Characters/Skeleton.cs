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
        public override float MovementSpeed { get; set; } = 1.5f;
        public override float MovementCount { get; set; } = 0;

        public Skeleton()
        {
            Damage = 2;
            Health = 10;
        }

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            if (anotherActor.Position == targetPosition)
            {
                return false;
            }
            return true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if(MovementCount >= MovementSpeed) this.HomingOnPlayer();
            SoundManager.Play(Songs.Skeltons);
            SoundManager.Playlist[Songs.Skeltons].loop = true;
        }


        protected override void OnDeath()
        {
            Player player = FindObjectOfType<Player>();
            player.SetScore(100);
            Debug.Log("Well, I was already dead anyway...");
        }

    }
}