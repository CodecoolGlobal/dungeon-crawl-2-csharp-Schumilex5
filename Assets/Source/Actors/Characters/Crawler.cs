using DungeonCrawl.Core;
using UnityEngine;
using System;
using Assets.Source.ExtensionMethods;
using DungeonCrawl.Actors;
using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    internal class Crawler : Character
    {
        // 353
        public static int damage = 1;
        private int health = 3;
        private readonly int[] _aniFrames = new[]{353, 354, 355, 356, 357, 358};
        private readonly int aniSpeed = 100;
        private int aniSteps = 0;

        public override int DefaultSpriteId => 353;
        public override string DefaultName => "Crawler";


        public override bool OnCollision(Actor actorAtPosition, (int, int) targetPosition)
        {
            if (actorAtPosition.GetType() == typeof(Player))
            {
                ((Player)actorAtPosition).ApplyDamage(damage);
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
            if (aniSteps >= (aniSpeed * _aniFrames.Length)) aniSteps = 0;
            else aniSteps++;

            int currentFrame = aniSteps / aniSpeed;

            SetSprite(_aniFrames[currentFrame]);
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I died");
        }

        private void TakeDamage(int amount)
        {
            health -= amount;
        }
    }
}
