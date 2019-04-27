using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Demo_ScrollUsingController : MonoBehaviour {

    public int secretEndingIndex;
    public int normalEndingIndex;
    public EndingEveNote endingNote;
    public GameManager gameManager;
    public NextLevelTrigger nextTrigger;

    // Next level trigger thing
    public bool isActive;
    public GameObject loadingScreenPanel;
    public FirstPersonController FPC;
    protected Slider loadingSlider;
    protected AsyncOperation async;

    // Use this for initialization
    void Start () {
		if (!endingNote)
        {
            endingNote = GetComponent<EndingEveNote>();
        }
        if (!nextTrigger)
        {
            nextTrigger = GetComponent<NextLevelTrigger>();
        }
        if (!gameManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        print("Has Scroll " + PlayerPrefs.GetInt("HasScroll"));
        if (PlayerPrefs.GetInt("HasScroll") != 0)
        {
            gameManager.endingNote = endingNote;
            nextTrigger.enabled = false;
        }
        else
        {
            this.enabled = false;
        }


        // Next level trigger thing
        loadingSlider = loadingScreenPanel.transform.Find("LoadingSlider").GetComponent<Slider>();
        loadingScreenPanel.SetActive(false);

        if (!FPC)
        {
            FPC = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        }
        FPC.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            if (gameManager.endingNote.getIsFinished())
            {
                isActive = false;
                if (gameManager.endingNote.optionIndexMade == 0)
                {
                    StartCoroutine(LoadingNextLevel(secretEndingIndex));
                }
                else
                {
                    StartCoroutine(LoadingNextLevel(normalEndingIndex));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isActive)
        {
            if (loadingScreenPanel)
            {
                gameManager.StartEndingNote();
                isActive = true;
            }
        }
    }

    IEnumerator LoadingNextLevel(int lvl)
    {
        loadingScreenPanel.SetActive(true);
        async = SceneManager.LoadSceneAsync(lvl);
        async.allowSceneActivation = false;
        FPC.enabled = false;

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
}
