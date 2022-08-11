using UnityEngine;
using System;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public static readonly int damage = 2;
        public new int Health { get; private set; } = 2;
        private readonly int moveTime = 500;
        private int moveTimer = 0;

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
            Player player = GameObject.FindObjectOfType<Player>();

            Debug.Log($"player pos: {player.Position}");
            Debug.Log($"selton pos: {Position}");
            if (player.Position.x >= Position.x && moveTimer >= moveTime)
            {
                TryMove(Direction.Down);
                moveTimer = 0;
            }

            if (player.Position.x <= Position.x && moveTimer >= moveTime)
            {
                TryMove(Direction.Up);
                moveTimer = 0;
            }

            if (player.Position.y >= Position.y && moveTimer >= moveTime)
            {
                TryMove(Direction.Right);
                moveTimer = 0;
            }

            if (player.Position.y >= Position.y && moveTimer >= moveTime)
            {
                TryMove(Direction.Left);
                moveTimer = 0;
            }



            moveTimer++;


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
