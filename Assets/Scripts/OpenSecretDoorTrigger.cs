using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSecretDoorTrigger : MonoBehaviour {

    public Note secretNote;
    public bool isActive;
    public GameObject secretDoor;
    public GameObject movablePlatform;

	// Use this for initialization
	void Start () {
		if (!secretNote)
        {
            secretNote = GetComponent<Note>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (secretNote)
        {
            int foundDiaryAmount = PlayerPrefs.GetInt("DiaryAmount");

            if (foundDiaryAmount >= 6 && !secretNote.canStartParagraph)
            {
                secretNote.canStartParagraph = true;
            }
            if (foundDiaryAmount < 6 && secretNote.canStartParagraph)
            {
                secretNote.canStartParagraph = false;
            }
        }

        if (movablePlatform)
        {
            int foundDiaryAmount = PlayerPrefs.GetInt("DiaryAmount");

            if (foundDiaryAmount >= 5 && !movablePlatform.activeInHierarchy)
            {
                movablePlatform.SetActive(true);
            }
            if (foundDiaryAmount < 5 && movablePlatform.activeInHierarchy)
            {
                movablePlatform.SetActive(false);
            }
        }

		if (!isActive)
        {
            if (secretNote.finished)
            {
                isActive = true;
                if (secretDoor)
                {
                    secretDoor.SetActive(isActive);
                }
            }
        }
	}
}
