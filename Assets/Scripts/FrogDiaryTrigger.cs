using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDiaryTrigger : MonoBehaviour {

    public bool isActive;
    public Diary diary;
    public GameManager gameManager;
    public Note note;


	// Use this for initialization
	void Start () {
		if (!gameManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        if (!note)
        {
            note = GetComponent<Note>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (!isActive)
        {
            int hasGua = gameManager.GetHasDua();
            if (hasGua != 0 && diary.getIsFinished()) 
            {
                isActive = true;
                gameManager.StartNote(note);
            }
        }
	}
}
