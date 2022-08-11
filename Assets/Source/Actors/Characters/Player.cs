using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Assets.Source.Actors.ExtensionMethods;
using Assets.Source.Core;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;


namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {

        public float timeBtwAttack { get; set; }
        public static readonly float startTimeBtwAttack = 1f;
        public static readonly int damage = 5;
        private List<Item> _inventory = new List<Item>();

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

            if (Input.GetKeyDown(KeyCode.E))
            {
                var item = ActorManager.Singleton.GetActorAt<Item>(Position);
                if (item != null)
                {
                    ItemPickUp(item);
                    Debug.Log($"{item.GetDurability()} and {item.GetBonusDmg()}");
                    ActorManager.Singleton.DestroyActor(item);
                }
                else
                {
                    UserInterface.Singleton.SetText("Nem vót itt item", UserInterface.TextPosition.TopLeft);
                }
            }
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                // Move right
                ShowInventory(_inventory);
            }
            
            CameraController.Singleton.Position = (Position.x,Position.y);
        }

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            if (anotherActor.GetType() == typeof(Skeleton))
            {
                anotherActor.AttackFromPlayer(this);
            }
<<<<<<< HEAD
            if (anotherActor.GetType() == typeof(Door) && _inventory.Any(x => x is Key))
            {
                Door door = ActorManager.Singleton.GetActorAt<Door>(targetPosition);
                ActorManager.Singleton.DestroyActor(door);
            }

=======
            
>>>>>>> c169d2d4f957a0814de96057c268455e0e7f9ee4
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

        private void ItemPickUp(Item newItem)
        {
            if (Position == newItem.Position)
            {
                if (_inventory.Count > 0)
                {
                    foreach (Item item in _inventory)
                    {
                        if (item.DefaultName == newItem.DefaultName)
                        {
                            item.ChangeDurability(newItem.GetDurability());
                            Debug.Log($"{newItem.DefaultName} gained durability");
                            Debug.Log($"{newItem.DefaultName} durability: {newItem.GetDurability()}");
                        }
                    }
                }
                else
                {
                    _inventory.Add(newItem);
                    Debug.Log($"{newItem.DefaultName} added to inventory");
                }
            }
        }
        

        private void ShowInventory(List<Item> inventory)
        {
            string items = "";
            foreach (var item in inventory)
            {
                    items += $"{item.DefaultName}: {item.GetDurability()}\n";
                    Debug.Log($"{item.DefaultName}: {item.GetDurability()}\n");
            }
            
            UserInterface.Singleton.SetText(items, UserInterface.TextPosition.BottomLeft);
            Debug.Log("Inventory shown");
        }
        

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
    }
}
