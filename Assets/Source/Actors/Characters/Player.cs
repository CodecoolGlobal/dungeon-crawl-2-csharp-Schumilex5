using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Source.Core;
using Assets.Source.Actors;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;
using Assets.Source.ExtensionMethods;
using System.Linq;
using UnityEngine.UIElements;


namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {

        private int _baseDamage;
        private int _bonusDamage;
        private List<Item> _inventory;
        private int _killedWizard;
        private static readonly Dictionary<string, int> _playerSpriteIDs = new Dictionary<string, int>
        {
            { "Default", 24 }, { "WithSowrd", 26 }
        };

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
        public int Score { get; private set; }
        public override float MovementSpeed { get; set; } = 0.1f;
        public override float MovementCount { get; set; } = 0;

        public Player()
        {
            _baseDamage = 5;
            _bonusDamage = 0;
            Damage = _baseDamage + _bonusDamage;
            Health = 20;
            Score = 0;
            _inventory = new List<Item>();
            _killedWizard = 0;
            //ShowStats();
        }

        public void SetScore(int points)
        {
            if (points > 0)
            {
                Score += points;
                TextDisplay.ShowScore(this);
            }
            else throw new OverflowException($"You can't take away points! Points: {points}");
        }

        private void IsItemHere()
        {
            if (ActorManager.Singleton.GetActorAt<Item>(Position))
            {
                Actor item = ActorManager.Singleton.GetActorAt<Item>(Position);
                string pickUpMsg = $"Press \"E\" to pick up {item.DefaultName}";
                StartCoroutine(TextDisplay.DisplayMessage(pickUpMsg, 2, UserInterface.TextPosition.BottomLeft));
            }
        }

        private void DoIHaveSword()
        {
            if (_inventory.Count > 0)
            {
                for (int item = 0; item < _inventory.Count; item++)
                {
                    Item thisItem = _inventory[item];
                    if (thisItem.GetType() == typeof(Sword) && thisItem.GetDurability() > 0)
                        _bonusDamage = thisItem.GetBonusDmg();
                }
            }
        }

        private void SetBonusDamage()
        {
            if (_inventory.Count > 0)
            {
                for (int item = 0; item < _inventory.Count; item++)
                {
                    Item thisItem = _inventory[item];
                    if (thisItem.GetType() == typeof(Sword) && thisItem.GetDurability() > 0) 
                        _bonusDamage = thisItem.GetBonusDmg();
                    else _bonusDamage = 0;
                }
            }
            else _bonusDamage = 0;
            
        }

        protected override void OnUpdate(float deltaTime)
        {
            if(MovementCount >= MovementSpeed) MoveMe();

            PickUpItem();

            ShowInventory();

            UsePotion();
            
            CamFollowPlayer();
        }

        public void CamFollowPlayer() => CameraController.Singleton.Position = (Position.x, Position.y);

        public void ShowInventory()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                StartCoroutine(TextDisplay.DisplayMessage(_inventory.GetInventory(), 1, UserInterface.TextPosition.MiddleRight));
            }
        }

        public void UsePotion()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                for (int i = 0; i < _inventory.Count; i++)
                {
                    if (_inventory[i].DefaultName == _inventory.GetItemFromInventory("Potion").DefaultName)
                    {
                        _inventory[i].ChangeDurability(_inventory, -1);
                        Health += 5;
                        SoundManager.Play(Songs.Eat);
                    }
                }
            }
        }

        public void PickUpItem()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var item = ActorManager.Singleton.GetActorAt<Item>(Position);
                if (item != null)
                {
                    ItemPickUp(item);
                    SetBonusDamage();
                    TextDisplay.ShowStats(Health, Damage);
                    SoundManager.Play(Songs.Collect);
                }
                else
                {
                    StartCoroutine(TextDisplay.DisplayMessage("No item here", 1, UserInterface.TextPosition.TopRight));
                }
            }
        }

        public void MoveMe()
        {
            if (Input.GetKey(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
                IsItemHere();
            }

            if (Input.GetKey(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
                IsItemHere();
            }

            if (Input.GetKey(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
                IsItemHere();
            }

            if (Input.GetKey(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
                IsItemHere();
            }
        }

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            var thingIFace = anotherActor.GetType();
            if (thingIFace.IsSubclassOf(typeof(Enemy)))
            {
                SetBonusDamage();
                Enemy enemyActor = (Enemy)ActorManager.Singleton.GetActorAt(targetPosition);
                Item swordItem = _inventory.GetItemFromInventory("Sword");

                if (swordItem != null)
                {
                    SoundManager.Play(Songs.Slash);
                    this.ApplyDmgOnPlayerAndEnemy(enemyActor);
                    swordItem.ChangeDurability(_inventory, swordItem.GetUseCost());
                }
                else
                {
                    this.ApplyDmgOnSelf(enemyActor.Damage);
                    SoundManager.Play(Songs.Punch);
                }

                TextDisplay.ShowStats(Health, Damage);
            }

            if (thingIFace == typeof(Door))
            {
                Item keyItem = _inventory.GetItemFromInventory("Key");
                int dur = 0;
                if (keyItem != null) dur = keyItem.GetDurability();
                else
                {
                    string noKey = "Get a key to open this door!";
                    StartCoroutine(TextDisplay.DisplayMessage(noKey, 1, UserInterface.TextPosition.BottomRight));
                }

                if (dur > 0)
                {
                    SoundManager.Play(Songs.Door);
                    Door door = ActorManager.Singleton.GetActorAt<Door>(targetPosition);
                    ActorManager.Singleton.DestroyActor(door);
                    keyItem.ChangeDurability(_inventory, keyItem.GetUseCost());
                }
            }
            return targetPosition == anotherActor.Position ? false : true;
        }

        protected override void OnDeath()
        {
            Debug.Log("Oh no, I'm dead!");
        }

        private void ItemPickUp(Item newItem)
        {
            List<Item> inventory = _inventory;
            if (newItem.GetType() == typeof(Sword)) SetSprite(_playerSpriteIDs["WithSowrd"]);

            bool itemExist = false;
            foreach (var item in inventory)
            {
                if (item.DefaultName == newItem.DefaultName)
                {
                    itemExist = true;
                    break;
                }
            }

            if (newItem.GetType() == typeof(Crown) && AllWizardDead())
            {
                int next = _killedWizard - 1;
                Utilities.NextLevel(next);
            }
            if (itemExist)
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].DefaultName == newItem.DefaultName)
                    {
                        inventory[i].ChangeDurability(inventory, newItem.GetDurability());
                        Debug.Log($"{newItem.DefaultName} gained durability");
                    }
                }
            }
            else
            {
                _inventory.Add(newItem);
                Debug.Log($"{newItem.DefaultName} added to inventory");
            }
            newItem.SetSprite(1);
        }

        public int GetScore() => Score;

        public bool AllWizardDead() => !FindObjectOfType<Wizard>() ? true : false;

        public void SetKilledWizardCount() => _killedWizard++;

        public void KillPlayer()
        {
            OnDeath();
            ActorManager.Singleton.DestroyActor(this);
        } 
    }
}
