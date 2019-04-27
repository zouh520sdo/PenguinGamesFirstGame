using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_HasScrollController : MonoBehaviour {

    public Note magicNote;
    public bool isActive;

	// Use this for initialization
	void Start () {
		if (!magicNote)
        {
            magicNote = GetComponent<Note>();
        }

        print("Has Scroll " + PlayerPrefs.GetInt("HasScroll"));

        PlayerPrefs.SetInt("HasScroll", 0);
        PlayerPrefs.Save();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isActive)
        {
            if (magicNote.getIsFinished())
            {
                PlayerPrefs.SetInt("HasScroll", 1);
                PlayerPrefs.Save();
                magicNote.gameObject.SetActive(false);
                isActive = true;
            }
        }
	}
}
