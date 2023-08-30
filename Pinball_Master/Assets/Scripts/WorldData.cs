using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "World", menuName = "SO/World", order = 0)]
public class WorldData : ScriptableObject
{
    public string worldName;
    public SceneAsset sceneAsset;
    public int stageCount;
    public Sprite displayTexture;
}

[Serializable]
public class WorldS
{
    public string worldName;
    public int stageCount;
}

[Serializable]
public class WorldList
{
    public WorldS[] _worldList;
}

public class Test
{
    public void Save(string path)
    {
        var file = File.ReadAllText(path);
        var data = JsonUtility.FromJson<WorldList>(file);

        JsonUtility.ToJson(file);
    }
}