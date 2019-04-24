using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    //public GameObject SettingPanel;
    public GameObject loadingScreenPanel;
    protected Slider loadingSlider;
    protected AsyncOperation async;

    public void Start()
    {
        if (loadingScreenPanel)
        {
            loadingSlider = loadingScreenPanel.transform.Find("LoadingSlider").GetComponent<Slider>();
            loadingScreenPanel.SetActive(false);
        }
    }

    IEnumerator LoadingNextLevel(string newGameLevel)
    {
        loadingScreenPanel.SetActive(true);
        async = SceneManager.LoadSceneAsync(newGameLevel);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            loadingSlider.value = async.progress;
            yield return null;
        }

        loadingSlider.value = loadingSlider.maxValue;
        //loadingScreenPanel.SetActive(false);
        while (!async.isDone)
        {
            if (async.progress == 0.9f)
            {
                loadingSlider.value = loadingSlider.maxValue;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void NewGameBtn(string newGameLevel)
    {
        if (loadingScreenPanel)
        {
            StartCoroutine(LoadingNextLevel(newGameLevel));
        }
        else
        {
            SceneManager.LoadScene(newGameLevel);
        }
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }

    public void SettingBtn()
    {
        //SettingPanel.SetActive(true);
    }

    public void BackBtn()
    {
       //SettingPanel.SetActive(false);
    }

    /*public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }*/
}
