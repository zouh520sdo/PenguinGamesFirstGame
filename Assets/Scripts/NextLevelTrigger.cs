using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class NextLevelTrigger : MonoBehaviour {

    public bool isActive;
    public GameObject loadingScreenPanel;
    public int loadingLvlIndex;
    public GameManager gameManager;
    public FirstPersonController FPC;
    public bool noNeedToTriggerOpeningAndEnding;
    public Note overridedEndingNote;
    protected Slider loadingSlider;
    protected AsyncOperation async;

    // Use this for initialization
    void Start () {
        loadingSlider = loadingScreenPanel.transform.Find("LoadingSlider").GetComponent<Slider>();
        loadingScreenPanel.SetActive(false);

        // Find game manager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (!FPC)
        {
            FPC = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        }
        FPC.enabled = true;

        //StartCoroutine(ShowingLoadScreen());
        if (!noNeedToTriggerOpeningAndEnding)
        {
            gameManager.StartOpeningNote();
        }
    }

    // Update is called once per frame
    void Update () {
        if (isActive)
        {
            if (overridedEndingNote)
            {
                if (overridedEndingNote.getIsFinished())
                {
                    isActive = false;
                    StartCoroutine(LoadingNextLevel(loadingLvlIndex));
                }
            }
            else
            {
                if (gameManager.endingNote.getIsFinished() || noNeedToTriggerOpeningAndEnding)
                {
                    isActive = false;
                    StartCoroutine(LoadingNextLevel(loadingLvlIndex));
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
                //loadingScreenPanel.SetActive(true);
                if (overridedEndingNote)
                {
                    gameManager.StartNote(overridedEndingNote);
                }
                else
                {
                    if (!noNeedToTriggerOpeningAndEnding)
                    {
                        gameManager.StartEndingNote();
                    }
                }
                isActive = true;
            }
        }
    }

    /*
    IEnumerator ShowingLoadScreen ()
    {
        
    }
    */

    IEnumerator LoadingNextLevel (int lvl)
    {
        loadingScreenPanel.SetActive(true);
        async = SceneManager.LoadSceneAsync(loadingLvlIndex);
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
