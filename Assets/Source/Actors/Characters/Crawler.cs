using UnityEngine;
using Assets.Source.ExtensionMethods;
using Assets.Source.Actors;

namespace DungeonCrawl.Actors.Characters
{
    internal class Crawler : Enemy
    {
        // 353
        private readonly int[] _aniFrames = new[] { 353, 354, 355, 356, 357, 358 };
        private readonly int _aniSpeed = 100;
        private int _aniSteps = 0;

        public override int DefaultSpriteId => 353;
        public override string DefaultName => "Crawler";

        public Crawler()
        {
            Damage = 1;
            Health = 1;
        }

        public override bool OnCollision(Actor actorAtPosition, (int, int) targetPosition)
        {
            if (actorAtPosition.GetType() == typeof(Player))
            {
                return false;
            }
            return true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            AnimateMovement();
            this.RandomMovementInsideMapBoundaries();
        }

        private void AnimateMovement()
        {
            int currentFrame = _aniSteps / _aniSpeed;

            if (currentFrame >= _aniFrames.Length)
            {
                _aniSteps = 0;
                currentFrame = 0;
            }
            
            SetSprite(_aniFrames[currentFrame]);
            _aniSteps++;
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I died");
        }

    }
}