using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utils.Server
{
    public class TestData : MonoBehaviour
    {
        [Serializable]
        public class Data : IJsonData
        {
            public string name;
            public int level;
            public int coin;
        }

        [Serializable]
        public class DataList : IJsonData
        {
            public List<Data> data;
        }

        [SerializeField] DataList _data;
        public string dataPath;
        public string fileName;

        void Start()
        {
            dataPath = Application.dataPath + "/Save/";

            if (!Directory.Exists(dataPath)) {
                Directory.CreateDirectory(dataPath);
            }

            LoadManager.LoadFromJson<Data>(dataPath + fileName);
        }

        public void SaveData()
        {
            string data = JsonUtility.ToJson(_data);
            File.WriteAllText(dataPath + fileName, data);
        }
        public DataList LoadData()
        {
            if (!File.Exists(dataPath + fileName))
                return null;

            var loadJson = File.ReadAllText(dataPath + fileName);
            _data = JsonUtility.FromJson<DataList>(loadJson);

            return _data;
        }

        public void Load()
        {
            LoadData();
        }
    }
}