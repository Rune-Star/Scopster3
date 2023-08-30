#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils.Server;
using Object = UnityEngine.Object;

public static class StaticFunc
{
    public static void Init(this GameManager gm, List<WorldData> b) { gm.worldDataList = b; }

    public static RectTransform rectTransform(this UIBehaviour uiBehaviour)
    {
        return uiBehaviour.GetComponent<RectTransform>();
    }

    public static void Load<T>(this Dictionary<string, T> dic, string path) where T : Object
    {
        if (!File.Exists(path)) {
            Directory.CreateDirectory(path);
            dic.Load(path); //
        }

        string json = File.ReadAllText(path);
        var dataSource = JsonUtility.FromJson<DictionaryData>(json);

        dic.Clear();

        foreach (var data in dataSource.dataTable) {
            dic.Add(data.ID, Resources.Load<T>(data.path));
        }
    }

    public static void Save<T>(this DictionaryData dic, Dictionary<string, GameObject> dictionary, string path)
    {
        dic.Init(dictionary);
        string json = JsonUtility.ToJson(dic);
        Debug.Log(json);
        File.WriteAllText(path, json);
    }

    public static void Save<T>(this DictionaryData<T> dic, string path)
    {
        string json = JsonUtility.ToJson(dic);

        File.WriteAllText(path, json);
    }

    public static void Modify(this Creature creature, PropertyAttribute attribute)
    {
        attribute.Modify(creature.stat);
    }
}
#endif