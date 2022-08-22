using DungeonCrawl.Core;
using UnityEngine;
using System;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        private readonly int moveTimeStep = 200;
        private int moveTimer = -2000;
        private int damage;
        public new int Health { get; }

        public Skeleton()
        {
            damage = 2;
            this.Health = 10;
        }

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
