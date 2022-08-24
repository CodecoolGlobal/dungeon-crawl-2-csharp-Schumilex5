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


        public static PlayerData LoadPlayerData()
        {
            string path = Application.persistentDataPath + "/player.txt";

            if (File.Exists(path))
            {
                Debug.Log("Létezik");
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;

            }
            else
            {
                string sadText = "No save file found, sorry :(";
                UserInterface.Singleton.SetText(sadText, UserInterface.TextPosition.MiddleCenter);
                return null;
            }
        }
    }
}