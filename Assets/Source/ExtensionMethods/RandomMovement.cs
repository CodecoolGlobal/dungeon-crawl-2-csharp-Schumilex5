using System.Threading.Tasks;
using UnityEngine;
using DungeonCrawl.Actors;
using DungeonCrawl;
using DungeonCrawl.Actors.Characters;
using UnityEngine.UIElements;

namespace Assets.Source.ExtensionMethods
{
    public static class RandomMovement
    {
        public static void MoveInRandomDirection(this Actor actor)
        {
            actor.TryMove(RandomDirection.GetRandomDirection());
        }        
    }

    public static class RandomMovementInsideMapBounds
    {
        // Map boundaries for NPCs
        // Min range
        private static readonly int _boundariesStartX = 0;
        private static readonly int _boundariesEndX = 30;

        //Max range
        private static readonly int _boundariesStartY = 20;
        private static readonly int _boundariesEndY = -20;

        public static void RandomMovementInsideMapBoundaries(this Actor actor)
        {
            if (   actor.Position.x > _boundariesStartX
                && actor.Position.x < _boundariesEndX
                && actor.Position.y < _boundariesStartY
                && actor.Position.y > _boundariesEndY
                )
            {
                actor.TryMove(RandomDirection.GetRandomDirection());
            }
        }
    }

    public static class HomingOnPlayerMovement
    {
        public static void HomingOnPlayer(this Actor actor)
        {
            Player player = GameObject.FindObjectOfType<Player>();

            if (player.Position.x > actor.Position.x)
            {
                actor.TryMove(Direction.Right);
            }
            else
            {
                actor.TryMove(Direction.Left);
            }

            if (player.Position.y < actor.Position.y)
            {
                actor.TryMove(Direction.Down);
            }
            else
            {
                actor.TryMove(Direction.Up);
            }
        }
    }

    public static class RandomDirection
    {
        // Gets a random direction value
        public static Direction GetRandomDirection()
        {
            int dirVal = GetRandomNumbers.GetRandomNumberBetweenTwoValues(0, 4);
            Direction direction = Direction.Up;

            switch (dirVal)
            {
                case 1:
                    direction = Direction.Down;
                    break;
                case 2:
                    direction = Direction.Left;
                    break;
                case 3:
                    direction = Direction.Right;
                    break;
            }
            return direction;
        }
    }
}
