using DungeonCrawl.Actors.Characters;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Source.Core;
using UnityEngine;

namespace Assets.Source.ExtensionMethods
{
    public class SaveSystem
    {
        public static void SavePlayerData(Player player)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.txt";
            Debug.Log(path);
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(player);
            
            formatter.Serialize(stream, data);
            stream.Close();
        }


        public static PlayerData ReadPlayerData()
        {
            string path = Application.persistentDataPath + "/player.txt";

            if (File.Exists(path))
            {
                Debug.Log("Létezik");
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                PlayerData data = (PlayerData) formatter.Deserialize(stream);
                Debug.Log($"{data.position[0]}, {data.position[1]}");
                Debug.Log($"{data.health}");
                stream.Close();

                return data;

            }
            else
            {
                string sadText = "No save file found, sorry :(";
                Debug.Log(sadText);
                return null;
            }
        }
    }
}