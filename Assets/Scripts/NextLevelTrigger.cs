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
    protected Slider loadingSlider;
    protected AsyncOperation async;

    // Use this for initialization
    void Start () {
        loadingSlider = loadingScreenPanel.transform.Find("LoadingSlider").GetComponent<Slider>();
        loadingScreenPanel.SetActive(true);

        // Find game manager
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (!FPC)
        {
            FPC = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        }

        async = SceneManager.LoadSceneAsync(loadingLvlIndex);
        async.allowSceneActivation = false;
        StartCoroutine(ShowingLoadScreen());
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            if (gameManager.endingNote.getIsFinished())
            {
                isActive = false;
                StartCoroutine(LoadingNextLevel(loadingLvlIndex));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (loadingScreenPanel)
            {
                //loadingScreenPanel.SetActive(true);
                gameManager.StartEndingNote();
                isActive = true;
            }
            else
            {
                SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
            }
        }
    }

    IEnumerator ShowingLoadScreen ()
    {
        FPC.enabled = false;
        while (async.progress < 0.9f)
        {
            loadingSlider.value = async.progress;
            yield return null;
        }
        loadingSlider.value = loadingSlider.maxValue;
        loadingScreenPanel.SetActive(false);
        FPC.enabled = true;
        gameManager.StartOpeningNote();
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
