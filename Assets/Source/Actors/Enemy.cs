using System;
using DungeonCrawl;
using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Items;
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

        public override void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);

            if (actorAtTargetPosition == null)
            {
                // No obstacle found, just move
                Position = targetPosition;
            }
            else
            {
                if (OnCollision(actorAtTargetPosition, targetPosition))
                {
                    // Allowed to move
                    Position = targetPosition;
                }
            }
        }
    }
}
