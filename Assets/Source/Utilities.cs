using DungeonCrawl.Core;
using System;
using Assets.Source.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawl.Actors.Characters;
using UnityEngine;

namespace DungeonCrawl
{
    public enum Direction : byte
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class TextDisplay
    {
        public static IEnumerator DisplayMessage(string message, int timeToDisplay, UserInterface.TextPosition textPlace)
        {
            UserInterface.Singleton.SetText(message, textPlace);
            yield return new WaitForSeconds(timeToDisplay);
            UserInterface.Singleton.SetText("", textPlace);
        }

        public static void ShowScore(Player player)
        {
            UserInterface.Singleton.SetText($"Score: {player.GetScore()}", UserInterface.TextPosition.MiddleLeft);
        }

        public static void ShowStats(int Health, int _damage)
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
    }

    public static class Utilities
    {

        public static (int x, int y) ToVector(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return (0, 1);
                case Direction.Down:
                    return (0, -1);
                case Direction.Left:
                    return (-1, 0);
                case Direction.Right:
                    return (1, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        public static void NextLevel(int next)
        {
            ActorManager.Singleton.DestroyAllActors();
            MapLoader.LoadMap(next);
        }
    }

    public class GetRandomNumbers
    {
        private static readonly System.Random random = new System.Random();
        
        public static int GetRandomNumberBetweenTwoValues(int from, int to)
        {
            return random.Next(from, to);
        }
    }
}
