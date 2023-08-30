using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    static string nextScene;
    AsyncOperation operation;

    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float minLoadingTime = 3f;

    float _endWidth;

    void Start()
    {
        progressBar.value = 0;

        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;

        SceneManager.LoadScene("Scenes/LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;

        float timer = 0;
        while (!operation.isDone && timer < minLoadingTime) {
            yield return null;

            timer += Time.deltaTime;

            if (operation.progress < 0.9f) {
                progressBar.value = Mathf.Lerp(operation.progress, 1f, timer);
                text.text = $"{operation.progress:P2}%";
                if (progressBar.value >= operation.progress)
                    timer = 0f;
            } else {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                text.text = "100%";
                if (progressBar.value >= 0.99f && timer > minLoadingTime) {
                    operation.allowSceneActivation = true;
                }
            }
        }
    }
}