using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public SaveLoadUtility slu;
    public OpeningEveNote openingNote;
    public EndingEveNote endingNote;
    public Wand wand;


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

        if (!wand)
        {
            wand = GameObject.FindGameObjectWithTag("Player").GetComponent<Wand>();
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
    }

    public void SaveGame()
    {
        slu.SaveGame(slu.quickSaveName);//Use this for quicksaving, which is basically just using a constant savegame name.
    }

    public void LoadGame()
    {
        slu.LoadGame(slu.quickSaveName);//Use this for quickloading, which is basically just using a constant savegame name.
    }

    public void StartOpeningNote()
    {
        wand.note = openingNote;
        wand.SetDialogueText(openingNote.nextLine());
    }

    public void StartEndingNote()
    {
        wand.note = endingNote;
        wand.SetDialogueText(endingNote.nextLine());
    }
}
