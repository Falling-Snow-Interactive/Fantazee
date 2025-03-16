using System.IO;
using Fantazee.Instance;
using UnityEngine;

namespace Fantazee.SaveLoad
{
    public static class SaveManager
    {
        private static string Path => $"{Application.persistentDataPath}/Saves";
        private const string Filename = "FantazeeRun.sav";
        private static string FilePath => $"{Path}/{Filename}";
        
        public static void SaveGame(GameInstance instance)
        {
            Debug.Log($"SaveManager: Saving");
            
            ValidateSaveFolder();
            
            GameSave save = new(instance);
            string serializedInstance = JsonUtility.ToJson(save, true);
            File.WriteAllText(FilePath, serializedInstance);
            
            Debug.Log($"SaveManager: Saved");
        }

        public static GameSave LoadGame()
        {
            Debug.Log($"SaveManager: Loading");
            
            ValidateSaveFolder();

            if (!File.Exists(FilePath))
            {
                return null;
            }
            
            string json = File.ReadAllText(FilePath);
            GameSave save = JsonUtility.FromJson<GameSave>(json);
            return save;
        }

        public static bool TryLoadGame(out GameSave save)
        {
            save = LoadGame();
            return save != null;
        }

        private static void ValidateSaveFolder()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }
    }
}