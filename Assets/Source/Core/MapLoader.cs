﻿using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static;
using System;
using System.Text.RegularExpressions;
using DungeonCrawl.Actors.Items;
using UnityEngine;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     MapLoader is used for constructing maps from txt files
    /// </summary>
    public static class MapLoader
    {
        /// <summary>
        ///     Constructs map from txt file and spawns actors at appropriate positions
        /// </summary>
        /// <param name="id"></param>
        public static void LoadMap(int id)
        {
            var lines = Regex.Split(Resources.Load<TextAsset>($"map_{id}").text, "\r\n|\r|\n");


            // Read map size from the first line
            var split = lines[0].Split(' ');
            var width = int.Parse(split[0]);
            var height = int.Parse(split[1]);

            // Create actors
            for (var y = 0; y < height; y++)
            {
                var line = lines[y + 1];
                for (var x = 0; x < width; x++)
                {
                    var character = line[x];

                    SpawnActor(character, (x, -y));
                }
            }

            // Set default camera size and position
            CameraController.Singleton.Size = 4;
            CameraController.Singleton.Position = (width / 2, -height / 2);
        }

        private static void SpawnActor(char c, (int x, int y) position)
        {
            switch (c)
            {
                case '#':
                    ActorManager.Singleton.Spawn<Wall>(position);
                    break;
                case '.':
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'p':
                    ActorManager.Singleton.Spawn<Player>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 's':
                    ActorManager.Singleton.Spawn<Skeleton>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'k': //Kard
                    ActorManager.Singleton.Spawn<Floor>(position);
                    ActorManager.Singleton.Spawn<Sword>(position);
                    break;
                case 'd': //Door
                    ActorManager.Singleton.Spawn<Floor>(position);
                    ActorManager.Singleton.Spawn<Door>(position);
                    break;
                case 'o': //Key
                    ActorManager.Singleton.Spawn<Floor>(position);
                    ActorManager.Singleton.Spawn<Key>(position);
                    break;
                case 'w': //Wizard
                    ActorManager.Singleton.Spawn<Wizard>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'c': //Crown
                    ActorManager.Singleton.Spawn<Floor>(position);
                    ActorManager.Singleton.Spawn<Crown>(position);
                    break;
                
                case 'h': //HelthPotion
                    ActorManager.Singleton.Spawn<Floor>(position);
                    ActorManager.Singleton.Spawn<Potion>(position);
                    break;
                case 'x': //Crawler
                    ActorManager.Singleton.Spawn<Crawler>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case ' ':
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
