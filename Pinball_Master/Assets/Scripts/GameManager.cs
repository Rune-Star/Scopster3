using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils.Server;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance => instance;

    public List<WorldData> worldDataList;

    //public List<PlayerWorldData> playerWorldDataList;
    public PlayerWorldDataFile playerWorldDataList;
    static PlayerWorldData selectedWorldPlayerData;
    public PlayerWorldData SelectedWorldPlayerData => selectedWorldPlayerData;

    public float deathTerm = 1f;
    public UnityUtils.Projectile.ProjectilePrefabs projectilePrefabs;

    public string dataPath;
    public string fileName;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dataPath = Application.dataPath + "/Save/";
        LoadData();
    }

    public void StartWorld()
    {
        SceneLoadManager.LoadScene(selectedWorldPlayerData.worldData.sceneAsset.name);

        var a =
            SceneManager.GetActiveScene().GetRootGameObjects().ToList();
        Debug.Log(a);
        var b = a.Find(o => o.GetComponent<Player>() != null);
        player = b.GetComponent<Player>();
        Debug.Log(b.name);
    }

    public void GoHome() { SceneLoadManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).name); }

    #region Save And Load

    public void LoadData()
    {
        var loadJson = File.ReadAllText(dataPath + fileName);
        var file = JsonUtility.FromJson<PlayerWorldDataFile>(loadJson);

        playerWorldDataList = file;

        foreach (var data in file._playerWorldData) {
            data.Print();
        }
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(playerWorldDataList);
        File.WriteAllText(dataPath + fileName, data);
    }

    public void SetSelectedWorld(PlayerWorldData data) { selectedWorldPlayerData = data; }

    [Serializable]
    public class PlayerWorldDataFile : IJsonData
    {
        public List<PlayerWorldData> _playerWorldData;
    }

    #endregion

    public Player player;
}