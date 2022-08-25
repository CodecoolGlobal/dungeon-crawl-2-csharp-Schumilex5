using System;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using UnityEngine;

namespace Assets.Source.Actors
{
    public abstract class Enemy : Character
    {
        public void KillEnemy()
        {
            OnDeath();
            ActorManager.Singleton.DestroyActor(this);
        }
    }
}
