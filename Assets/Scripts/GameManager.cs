using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public SaveLoadUtility slu;
    public OpeningEveNote openingNote;
    public EndingEveNote endingNote;
    public Wand wand;
    public FirstPersonController fpc;


    public bool noNeedToTriggerOpeningAndEnding;


    private void Awake()
    {
        tag = "GameManager";

        if (!openingNote)
        {
            openingNote = GetComponent<OpeningEveNote>();
        }

        if (!endingNote)
        {
            endingNote = GetComponent<EndingEveNote>();
        }

        if (!fpc)
        {
            fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        }

        if (SceneManager.GetActiveScene().name == "FirstLevel")
        {
            ResetDiaryAmount();
            ResetHasGua();
            ResetFoundThisDiary();
        }

        if (!wand)
        {
            wand = GameObject.FindGameObjectWithTag("Player").GetComponent<Wand>();
        }
    }
    // Use this for initialization
    void Start () {
        if (slu == null)
        {
            slu = GetComponent<SaveLoadUtility>();
            if (slu == null)
            {
                Debug.Log("[SaveLoadMenu] Start(): Warning! SaveLoadUtility not assigned!");
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        //The classic hotkeys for quicksaving and quickloading
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }

        if (Input.GetKeyDown(KeyCode.C))  // Set 6 diary
        {
            ResetDiaryAmount();
            UpdateDiaryAmount();
            UpdateDiaryAmount();
            UpdateDiaryAmount();
            UpdateDiaryAmount();
            UpdateDiaryAmount();
            UpdateDiaryAmount();
            SetFoundThisDiary();
            SetHasGua();
            print("Diary amount " + GetDiaryAmount());
            print("Found this diary " + GetFoundThisDiary());
            print("Has Gua " + GetHasGua());
        }

        if (Input.GetKeyDown(KeyCode.X))  // Set 5 diary
        {
            ResetDiaryAmount();
            UpdateDiaryAmount();
            UpdateDiaryAmount();
            UpdateDiaryAmount();
            UpdateDiaryAmount();
            UpdateDiaryAmount();
            SetFoundThisDiary();
            SetHasGua();
            print("Diary amount " + GetDiaryAmount());
            print("Found this diary " + GetFoundThisDiary());
            print("Has Gua " + GetHasGua());
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ResetDiaryAmount();
            ResetFoundThisDiary();
            ResetHasGua();
            print("Diary amount " + GetDiaryAmount());
            print("Found this diary " + GetFoundThisDiary());
            print("Has Gua " + GetHasGua());
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            print("Diary amount " + GetDiaryAmount());
            print("Found this diary " + GetFoundThisDiary());
            print("Has Gua " + GetHasGua());
        }
    }

    public void SaveGame()
    {
        slu.SaveGame(slu.quickSaveName);//Use this for quicksaving, which is basically just using a constant savegame name.
    }

    public void LoadGame()
    {
        slu.LoadGame(slu.quickSaveName);//Use this for quickloading, which is basically just using a constant savegame name.
    }

    public void StartNote(Note note)
    {
        if (wand.note != note)
        {
            fpc.enabled = false;
            wand.note = note;
            wand.SetDialogueText(note.nextLine());
        }
    }

    public void StartOpeningNote()
    {
        if (wand.note != openingNote && !noNeedToTriggerOpeningAndEnding)
        {
            Debug.Log("fpc.enabled = false in StartOpeningNote");
            fpc.enabled = false;
            wand.note = openingNote;
            wand.SetDialogueText(openingNote.nextLine());
        }
    }

    public void StartEndingNote()
    {
        if (wand.note != endingNote && !noNeedToTriggerOpeningAndEnding)
        {
            fpc.enabled = false;
            wand.note = endingNote;
            wand.SetDialogueText(endingNote.nextLine());
        }
    }

    public void ResetDiaryAmount()
    {
        PlayerPrefs.SetInt("DiaryAmount", 0);
        PlayerPrefs.Save();
    }

    public void UpdateDiaryAmount()
    {
        int tempDiaryAmount = PlayerPrefs.GetInt("DiaryAmount");
        tempDiaryAmount++;
        PlayerPrefs.SetInt("DiaryAmount", tempDiaryAmount);
        PlayerPrefs.Save();
    }

    public int GetDiaryAmount()
    {
        return PlayerPrefs.GetInt("DiaryAmount");
    }

    public void ResetHasGua()
    {
        PlayerPrefs.SetInt("HasGua", 0);
        PlayerPrefs.Save();
    }
    public void SetHasGua()
    {
        PlayerPrefs.SetInt("HasGua", 1);
        PlayerPrefs.Save();
    }
    public int GetHasGua()
    {
        return PlayerPrefs.GetInt("HasGua");
    }

    public void ResetFoundThisDiary()
    {
        PlayerPrefs.SetInt("FoundThisDiary", 0);
        PlayerPrefs.Save();
    }
    public void SetFoundThisDiary()
    {
        PlayerPrefs.SetInt("FoundThisDiary", 1);
        PlayerPrefs.Save();
    }
    public int GetFoundThisDiary()
    {
        return PlayerPrefs.GetInt("FoundThisDiary");
    }
}
