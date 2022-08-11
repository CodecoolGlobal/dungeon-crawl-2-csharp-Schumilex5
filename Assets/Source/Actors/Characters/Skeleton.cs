using UnityEngine;
using System;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public static readonly int damage = 2;
        public new int Health { get; private set; } = 2;
        private readonly int moveTimeStep = 200;
        private int moveTimer = 0;

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            if (anotherActor.Position == targetPosition)
            {
                return false;
            }
            ResetMoveTimer();
            return true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            Player player = GameObject.FindObjectOfType<Player>();

            if (moveTimer >= moveTimeStep)
            {
                if (player.Position.x > Position.x )
                {
                    TryMove(Direction.Right);
                }
                else
                {
                    TryMove(Direction.Left);
                }

                if (player.Position.y < Position.y)
                {
                    TryMove(Direction.Down);
                }
                else
                {
                    TryMove(Direction.Up);
                }
            }

            ResetMoveTimer();
        }

        private void ResetMoveTimer()
        {
            if (moveTimer >= moveTimeStep)
            {
                moveTimer=0;
            }
            else
            {
                moveTimer++;
            }
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
