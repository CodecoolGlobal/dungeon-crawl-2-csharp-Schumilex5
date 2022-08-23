using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Source.Core;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;
using System.Linq;
using UnityEngine.UIElements;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {

        private int _baseDamage;
        private int _bonusDamage;
        private List<Item> _inventory;
        private int _damage;
        private int _killedWizard;
        private readonly int _stepTimer = 150;
        private int _stepCount = 0;
        private static readonly Dictionary<string, int> playerSpriteIDs = new Dictionary<string, int>
        {
            { "Default", 24 }, { "WithSowrd", 26 }
        };

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";
        public int Score { get; private set; }

        public Player()
        {
            _baseDamage = 5;
            _bonusDamage = 0;
            _damage = _baseDamage + _bonusDamage;
            Health = 20;
            Score = 0;
            _inventory = new List<Item>();
            _killedWizard = 0;
            ShowStats();
        }

        public void SetScore(int points)
        {
            if (points > 0)
            {
                Score += points;
                ShowScore();
            }
            else
            {
                throw new OverflowException($"You can't take away points! Points: {points}");
            }
        }

        public void ShowStats()
        {
            if (Health > 0)
            {
                UserInterface.Singleton.SetText($"Health: {Health}\nDamage: {_damage}", UserInterface.TextPosition.TopLeft);
            }
            else
            {
                UserInterface.Singleton.SetText("Oh no I'm dead".ToUpper(), UserInterface.TextPosition.TopCenter);
                
            }
        }

        private void ShowScore()
        {
            UserInterface.Singleton.SetText($"Score: {GetScore()}", UserInterface.TextPosition.MiddleLeft);
        }

        private void IsItemHere()
        {
            if (ActorManager.Singleton.GetActorAt<Item>(Position))
            {
                Actor item = ActorManager.Singleton.GetActorAt<Item>(Position);
                string pickUpMsg = $"Press \"E\" to pick up {item.DefaultName}";
                StartCoroutine(DisplayMessage(pickUpMsg, 2, UserInterface.TextPosition.BottomLeft));
            }
        }
        
        IEnumerator DisplayMessage(string message, int timeToDisplay, UserInterface.TextPosition textPlace)
        {
            UserInterface.Singleton.SetText(message, textPlace);
            yield return new WaitForSeconds(timeToDisplay);
            UserInterface.Singleton.SetText("", textPlace);

        }

        private void DoIHaveSword()
        {
            if (_inventory.Count > 0)
            {
                for (int item = 0; item < _inventory.Count; item++)
                {
                    Item thisItem = _inventory[item];
                    if (thisItem.GetType() == typeof(Sword) && thisItem.GetDurability() > 0)
                    {
                        _bonusDamage = thisItem.GetBonusDmg();
                    }
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
                    {
                        _bonusDamage = thisItem.GetBonusDmg();
                    }
                    else
                    {
                        _bonusDamage = 0;
                    }
                }
            }
            else
            {
                _bonusDamage = 0;
            }
        }

        private int CalculateDamage()
        {
            _damage = _baseDamage + _bonusDamage;
            return _damage;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_stepCount >= _stepTimer)
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
                _stepCount = 0;
            }
            else
            {
                _stepCount++;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                var item = ActorManager.Singleton.GetActorAt<Item>(Position);
                if (item != null)
                { 
                    ItemPickUp(item);
                    SetBonusDamage();
                    ShowStats();

                }
                else
                {
                    StartCoroutine(DisplayMessage("No item here", 1, UserInterface.TextPosition.TopRight));
                }
            }
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                // Move right
                StartCoroutine(DisplayMessage(GetInventory(_inventory), 1, UserInterface.TextPosition.MiddleRight));
            }
            
            CameraController.Singleton.Position = (Position.x,Position.y);
        }

        public override bool OnCollision(Actor anotherActor, (int, int) targetPosition)
        {
            var thingIFace = anotherActor.GetType();
            if (thingIFace == typeof(Skeleton))
            {
                SetBonusDamage();
                Actor skeleton = ActorManager.Singleton.GetActorAt(targetPosition);
                Item swordItem = GetItemFromInventory("Sword");
                if (swordItem != null)
                {
                    AttackSkeleton(skeleton);
                    swordItem.ChangeDurability(_inventory, swordItem.GetUseCost());
                    ShowStats();
                }
                else
                {
                    AttackSkeleton(skeleton);
                    ShowStats();
                }

            }

            if(thingIFace == typeof(Crawler))
            {
                SetBonusDamage();
                Actor enemy = ActorManager.Singleton.GetActorAt(targetPosition);
                Crawler crawlerEnemey = (Crawler)enemy;
                crawlerEnemey.ApplyDamage(_damage);
                ApplyDamage(Crawler.damage);
            }
            
            if (thingIFace == typeof(Wizard))
            {
                Debug.Log("This is a wizard here");
                SetBonusDamage();
                Actor wizard = ActorManager.Singleton.GetActorAt(targetPosition);
                Item swordItem = GetItemFromInventory("Sword");
                if (swordItem != null)
                {
                    AttackWizard(wizard);
                    swordItem.ChangeDurability(_inventory, swordItem.GetUseCost());
                    ShowStats();
                }
                else
                {
                    AttackWizard(wizard);
                    ShowStats();
                }

            }
            if (thingIFace == typeof(Door))
            {
                Item keyItem = GetItemFromInventory("Key");
                int dur = 0;
                if (keyItem != null)
                {
                    dur = keyItem.GetDurability();
                }
                else
                {
                    string noKey = "Get a key to open this door!";
                    StartCoroutine(DisplayMessage(noKey, 1, UserInterface.TextPosition.BottomRight));
                }

                if (dur > 0)
                {
                    Door door = ActorManager.Singleton.GetActorAt<Door>(targetPosition);
                    ActorManager.Singleton.DestroyActor(door);
                    keyItem.ChangeDurability(_inventory, keyItem.GetUseCost());
                }
                
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

        private void ItemPickUp(Item newItem)
        {
            List<Item> inventory = _inventory;
            if (newItem.GetType() == typeof(Sword)) SetSprite(playerSpriteIDs["WithSowrd"]);

            if (newItem.GetType() == typeof(Crown) && AllWizardDead())
            {
                int next = _killedWizard - 1;
                NextLevel(next);
            }
            if (_inventory.Count > 0)
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].DefaultName == newItem.DefaultName)
                    {
                        inventory[i].ChangeDurability(inventory, newItem.GetDurability());
                        Debug.Log($"{newItem.DefaultName} gained durability");
                    }
                    else
                    {
                        _inventory.Add(newItem);
                       
                        Debug.Log($"{newItem.DefaultName} added to inventory");
                        break;
                    }
                }
            }
            else
            {
                _inventory.Add(newItem);
                Debug.Log($"{newItem.DefaultName} added to inventory cause inventory was empty");
            }

            newItem.SetSprite(1);
        }
        

        private string GetInventory(List<Item> inventory)
        {
            string items = "";
            foreach (Item item in inventory)
            {
                    items += $"{item.DefaultName}: {item.GetDurability()}\n";
                    Debug.Log($"{item.DefaultName}: {item.GetDurability()}\n");
            }

            return items;
        }

        private int GetHealth()
        {
            return Health;
        }

        private int GetScore()
        {
            return Score;
        }
        

        private Item GetItemFromInventory(string className)
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                if (_inventory[i].DefaultName == $"{className}")
                {
                    return _inventory[i];
                }
            }

            return null;
        }


        public void AttackSkeleton(Actor enemy)
        {
            ((Skeleton)enemy).ApplyDamage(CalculateDamage());
            ApplyDamage(((Skeleton)enemy).GetDamage());
            //UserInterface.Singleton.SetText(ShowHealth(), UserInterface.TextPosition.MiddleLeft);
        }
        
        private void AttackWizard(Actor enemy)
        {
            ((Wizard)enemy).ApplyDamage(CalculateDamage());
            ApplyDamage(((Wizard)enemy).GetDamage());
        }
        
        public bool AllWizardDead()
        {
            if (!FindObjectOfType<Wizard>())
            {
                return true;
            }

            return false;
        }

        public void SetKilledWizardCount()
        {
            _killedWizard += 1;
        }

        private void NextLevel(int next)
        {
            ActorManager.Singleton.DestroyAllActors();
            MapLoader.LoadMap(next);
        }

        

        
    }
    
}
