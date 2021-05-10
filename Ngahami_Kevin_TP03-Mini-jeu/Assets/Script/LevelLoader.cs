using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public GameObject loadScreen;
    public Slider slider;
    public Text text;

    void LoadLevel(int sceneIndex)
    {
        //AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        //async.
        StartCoroutine(Async(sceneIndex));
    }

    IEnumerator Async(int sceneIndex)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        loadScreen.SetActive(true);

        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progress;
            text.text = progress * 100 + "%";
            yield return null;
        }
    }
}
