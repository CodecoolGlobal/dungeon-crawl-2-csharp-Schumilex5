using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using DungeonCrawl.Actors;
using DungeonCrawl.Core;
using UnityEngine;

namespace Assets.Source.ExtensionMethods
{
    public static class SaveSystem
    {
        public static void SaveGame()
        {
            HashSet<Actor> allActor = ActorManager.Singleton.GetAllActor();
            List<Actor> actorList = allActor.ToList();

            var path = Application.persistentDataPath + "/actorData.lofasz";
            Stream stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, actorList);
            stream.Close();

        }

        public static HashSet<Actor> LoadGame()
        {
            var path = Application.persistentDataPath + "/actorData.lofasz";

            if (File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                List<Actor> allActorDataList = binaryFormatter.Deserialize(stream) as List<Actor>;

                if (allActorDataList != null)
                {
                    HashSet<Actor> allActorData = new HashSet<Actor>(allActorDataList);
                    stream.Close();
                    return allActorData;
                }
                else
                {
                    stream.Close();
                    Debug.Log("No data found");
                    return null;
                }
            }
            else
            {
                Debug.Log("No save file found");
                return null;
            }
        }
    }
}