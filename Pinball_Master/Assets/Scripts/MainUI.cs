using System;
using TMPro;
using Utils;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerWorldData : Utils.Server.IJsonData
{
    public WorldData worldData;
    public int maxStage;

    public void Print() { Debug.Log($"{worldData}, {maxStage.ToString()}"); }
}

public class MainUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI worldName_text;
    [SerializeField] TextMeshProUGUI maxStageNumber_text;
    [SerializeField] Image worldMap_image;

    [SerializeField] Button startButton;

    void Awake() { startButton.onClick.AddListener(GameManager.Instance.StartWorld); }

    public void Init()
    {
        var playerData = GameManager.Instance.SelectedWorldPlayerData;
        var index = GameManager.Instance.playerWorldDataList._playerWorldData.IndexOf(playerData);

        worldName_text.text = $"<b>{(index + 1).ToString()}. {playerData.worldData.worldName}</b>";
        maxStageNumber_text.text
            = $"Max Stage <b>{playerData.maxStage.ToString()}/{playerData.worldData.stageCount.ToString()}</b>";
        worldMap_image.sprite = playerData.worldData?.displayTexture;
    }
}