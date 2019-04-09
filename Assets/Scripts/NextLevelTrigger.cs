using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelTrigger : MonoBehaviour {

    public GameObject loadingScreenPanel;
    public int loadingLvlIndex;
    protected Slider loadingSlider;
    protected AsyncOperation async;

    // Use this for initialization
    void Start () {
        loadingSlider = loadingScreenPanel.transform.Find("LoadingSlider").GetComponent<Slider>();
        loadingScreenPanel.SetActive(true);

        async = SceneManager.LoadSceneAsync(loadingLvlIndex);
        async.allowSceneActivation = false;
        StartCoroutine(ShowingLoadScreen());
    }
	
	// Update is called once per frame
	void Update () {
		
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (loadingScreenPanel)
            {
                //loadingScreenPanel.SetActive(true);
                StartCoroutine(LoadingNextLevel(loadingLvlIndex));
            }
            else
            {
                SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
            }
        }
    }

    IEnumerator ShowingLoadScreen ()
    {
        while (async.progress < 0.9f)
        {
            loadingSlider.value = async.progress;
            yield return null;
        }
        loadingSlider.value = loadingSlider.maxValue;
        loadingScreenPanel.SetActive(false);
    }

    IEnumerator LoadingNextLevel (int lvl)
    {
        while (!async.isDone)
        {
            loadingSlider.value = async.progress;
            if (async.progress == 0.9f)
            {
                loadingSlider.value = loadingSlider.maxValue;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
