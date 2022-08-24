using Assets.Source.Actors;
using UnityEngine;
using Assets.Source.ExtensionMethods;

namespace DungeonCrawl.Actors.Characters
{
    public class Wizard : Enemy
    {
        public new int Health { get; }

        public Wizard()
        {
            Damage = 5;
            Health = 50;
        }

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            return false;
        }

        protected override void OnUpdate(float deltaTime)
        {
            this.MoveInRandomDirection();
        }

        protected override void OnDeath()
        {
            Player player = FindObjectOfType<Player>();
            player.SetScore(250);
            player.SetKilledWizardCount();
            Debug.Log("I will live forever in another dimension anyway..");
        }

        public override int DefaultSpriteId => 71;
        public override string DefaultName => "Wizard";
    }
}