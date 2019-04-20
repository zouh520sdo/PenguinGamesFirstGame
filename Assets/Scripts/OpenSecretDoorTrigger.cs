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

        if (secretDoor)
        {
            secretDoor.SetActive(isActive);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (secretNote && !secretNote.canStartParagraph)
        {
            int foundDiaryAmount = PlayerPrefs.GetInt("FoundThisDiary");

            if (foundDiaryAmount >= 6)
            {
                secretNote.canStartParagraph = true;
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
