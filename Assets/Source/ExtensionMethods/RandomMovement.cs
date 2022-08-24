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
        // Movement parameters
        private static readonly int _stepTimer = 700; // Speed of walking
        private static int _stepCount = 0; // Counts up to the stepTimer then gets reset to 0

        public static void MoveInRandomDirection(this Actor actor)
        {
            if (_stepCount >= _stepTimer)
            {
                actor.TryMove(RandomDirection.GetRandomDirection()); 
                _stepCount = 0;
            }
            else _stepCount++;
        }        
    }

    public static class RandomMovementInsideMapBounds
    {
        // Movement parameters
        private static readonly int _stepTimer = 500; // Speed of walking
        private static int _stepCount = 0; // Counts up to the stepTimer then gets reset to 0

        // Map boundaries for NPCs
        // Min range
        private static readonly int _boundariesStartX = 0;
        private static readonly int _boundariesEndX = 30;

        //Max range
        private static readonly int _boundariesStartY = 20;
        private static readonly int _boundariesEndY = -20;

        public static void RandomMovementInsideMapBoundaries(this Actor actor)
        {
            if (_stepCount >= _stepTimer
                && actor.Position.x > _boundariesStartX
                && actor.Position.x < _boundariesEndX
                && actor.Position.y < _boundariesStartY
                && actor.Position.y > _boundariesEndY
                )
            {
                actor.TryMove(RandomDirection.GetRandomDirection());
                _stepCount = 0;
            }
            else _stepCount++;
            
        }
    }

    public static class HomingOnPlayerMovement
    {
        private static readonly int _stepTimer = 1000; // Speed of walking
        private static int _stepCount = -2000; // Counts up to the stepTimer then gets reset to 0

        public static void HomingOnPlayer(this Actor actor)
        {
            Player player = GameObject.FindObjectOfType<Player>();

            if (_stepCount >= _stepTimer)
            {
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
                _stepCount = 0;
            }
            else
            {
                _stepCount++;
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
