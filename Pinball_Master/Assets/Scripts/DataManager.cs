using System.Collections.Generic;
using System.IO;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utils.Server
{
    public class DataManager : MonoBehaviour
    {
        static DataManager instance;

        public static DataManager Instance => instance;

        public List<WorldData> worldDataList;
        public List<PlayerWorldData> playerWorldDataList;
        public PlayerWorldData selectedWorldPlayerData;

        public DictionaryData dicData;

        [SerializedDictionary("ID", "Prefab")] public SerializedDictionary<string, GameObject> prefabDict;
        Dictionary<string, GameObject> dict;
        
        string path;
        public TextAsset textAsset;

        void Awake()
        {
            instance = this;

            if (instance == null) {
                instance = FindFirstObjectByType<DataManager>();
                if (instance == null) {
                    var obj = new GameObject().AddComponent<DataManager>();
                    DontDestroyOnLoad(obj.gameObject);
                }
            }
        }
        void Start()
        {
            path = AssetDatabase.GetAssetPath(textAsset);

            prefabDict.Load(path);
            dict = prefabDict;

            WorldData worldData = new WorldData();
            //LoadManager.Instance.LoadFromJson<WorldData>();
        }

        public T GetPrefab<T>() where T : Component
        {
            T result = null;
            //var obj = prefabDict.Values.OfType<T>().ToList();
            var obj = Enumerable.First(dict.Values, o => o.TryGetComponent<T>(out result));
            return result;
        }

        public GameObject GetPrefab<T>(string id)
        {
            var obj = dict[id];
            return obj;
        }

        T LoadFromJson<T>(string path)
        {
            if (!File.Exists(path))
                throw new UnityException("NO FILE");

            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);
            print(data);
            //JsonConvert.ToString(data,);

            return data;
        }

        void SaveToJson(object obj, string path)
        {
            //string json = JsonConvert.SerializeObject(obj);
            string json = JsonUtility.ToJson(obj);

            File.WriteAllText(path, json);
            print(json);
        }

        public void SaveButton() { dicData.Save<GameObject>(dict, path); }
        public void LoadButton() { dict.Load(path); }
    }

    [System.Obsolete]
    [System.Serializable]
    public class KeyValuePair2List<K, V>
    {
        public List<K> Key;
        public List<V> Value;

        public void Init(Dictionary<K, V> list)
        {
            Key = list.Keys.ToList();
            Value = list.Values.ToList();
        }
    }

    [System.Serializable]
    public class DictionaryData
    {
        public Dictionary<string, GameObject> dict;
        public List<Data> dataTable;

        [System.Serializable]
        public class Data
        {
            public string ID;
            public string path;

            public Data(string ID, string path)
            {
                this.ID = ID;
                this.path = path;
            }
        }

        public void Init(Dictionary<string, GameObject> dic)
        {
            var key = dic.Keys.ToList();
            var value = dic.Values.ToList();

            dataTable = new List<Data>(dic.Count);

            for (int index = 0; index < dic.Count; index++) {
                dataTable.Add(new Data(key[index], AssetDatabase.GetAssetPath(value[index])));
            }
        }
    }

    [System.Serializable]
    public class DictionaryData<T>
    {
        public List<Data> dataTable;

        [System.Serializable]
        public class Data
        {
            public string ID;
            public T item;
            public string path;
        }
    }

    #region Nothing

    // public interface IDataSource<out T> where T : IDataSource<T>
    // {
    //     T LoadData();
    // }
    //
    // public class WorldData : IDataSource<WorldData>
    // {
    //     public string worldName;
    //     public string sceneAssetPath;
    //     public int stageCount;
    //     public string spritePath;
    //     
    //     public WorldData LoadData() { return this; }
    //     public void Save() { }
    // }

    #endregion
}