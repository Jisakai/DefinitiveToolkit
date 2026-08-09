using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DTK.Core.Save
{
    public class SaveManager
    {
        private readonly Dictionary<string, ISaveable> _saveables = new Dictionary<string, ISaveable>();

        #region Registration
        public void Register(ISaveable saveable)
        {
            if (_saveables.ContainsKey(saveable.SaveId))
            {
                Debug.LogWarning($"[SaveManager] Overwriting registered saveable: {saveable.SaveId}");
            }
            _saveables[saveable.SaveId] = saveable;
        }

        public void Unregister(ISaveable saveable)
        {
            _saveables.Remove(saveable.SaveId);
        }
        #endregion

        #region Save
        public void Save(string slotName)
        {
            SaveFile file = new SaveFile();

            foreach (KeyValuePair<string, ISaveable> kvp in _saveables)
            {
                file.entries.Add(new SaveEntry
                {
                    id = kvp.Key,
                    json = kvp.Value.CaptureState()
                });
            }

            string json = JsonUtility.ToJson(file, true);
            File.WriteAllText(GetPath(slotName), json);

            Debug.Log($"[SaveManager] Saved '{slotName}' with {file.entries.Count} entries.");
        }
        #endregion

        #region Load
        public bool Load(string slotName)
        {
            string path = GetPath(slotName);

            if (!File.Exists(path))
            {
                Debug.LogWarning($"[SaveManager] No save file found at slot '{slotName}'.");
                return false;
            }

            string json = File.ReadAllText(path);
            SaveFile file = JsonUtility.FromJson<SaveFile>(json);

            foreach (SaveEntry entry in file.entries)
            {
                if (_saveables.TryGetValue(entry.id, out ISaveable saveable))
                {
                    saveable.RestoreState(entry.json);
                }
                else
                {
                    Debug.LogWarning($"[SaveManager] No registered saveable for id '{entry.id}'; skipping.");
                }
            }

            Debug.Log($"[SaveManager] Loaded '{slotName}'.");
            return true;
        }
        #endregion

        #region Utility
        public bool SlotExists(string slotName) => File.Exists(GetPath(slotName));

        public void DeleteSlot(string slotName)
        {
            string path = GetPath(slotName);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log($"[SaveManager] Deleted slot '{slotName}'.");
            }
        }

        private string GetPath(string slotName) => Path.Combine(Application.persistentDataPath, slotName + ".json");
        #endregion

        #region Data
        [System.Serializable]
        private class SaveEntry
        {
            public string id;
            public string json;
        }

        [System.Serializable]
        private class SaveFile
        {
            public List<SaveEntry> entries = new List<SaveEntry>();
        }
        #endregion
    }
}