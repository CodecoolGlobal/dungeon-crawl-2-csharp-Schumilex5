using UnityEngine;
using Assets.Source.Actors.ExtensionMethods;
using DungeonCrawl.Core;


namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {

        public float timeBtwAttack { get; set; }
        public static readonly float startTimeBtwAttack = 1f;
        public static readonly int damage = 5;

        protected override void OnUpdate(float deltaTime)
        {


            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
            }
            CameraController.Singleton.Position = (Position.x,Position.y) ;
        }

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            if (anotherActor.GetType() == typeof(Skeleton))
            {
                anotherActor.AttackFromPlayer(this);
            }
            
            if (targetPosition == anotherActor.Position)
            {
                return false;
            }
            return true;
        }

        

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}
