using UnityEngine;


namespace DungeonCrawl.Actors.Characters
{
    public class Wizard : Character
    {
        private int _damage;
        public new int Health { get; }

        public Wizard()
        {
            _damage = 5;
            Health = 50;
        }

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            return true;
        }

        protected override void OnDeath()
        {
            Player player = FindObjectOfType<Player>();
            player.SetScore(250);
            player.SetKilledWizardCount();
            Debug.Log("I will live forever in another dimension anyway..");
        }

        public int GetDamage()
        {
            return _damage;
        }
        public int GetHealth()
        {
            return Health;
        }

        public override int DefaultSpriteId => 71;
        public override string DefaultName => "Wizard";
    }
}