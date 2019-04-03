using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePicture : MonoBehaviour {

    public bool isRed;
    public bool isHandsUp;
    public Note note;

	// Use this for initialization
	void Start () {
		if (!note)
        {
            note = GetComponentInChildren<Note>();
        }
        SetNote();
    }

    public void SetNote()
    {
        string temp = "";
        if (isRed)
        {
            temp += "I'm red, ";
        }
        else
        {
            temp += "I'm blue, ";
        }

        if (isHandsUp)
        {
            temp += "and I see two red.";
        }
        else
        {
            temp += "and I don't see two red.";
        }
        
        if (note)
        {
            note.note = temp;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
