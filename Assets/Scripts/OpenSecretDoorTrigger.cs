using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSecretDoorTrigger : MonoBehaviour {

    public Note secretNote;
    public bool isActive;

	// Use this for initialization
	void Start () {
		if (!secretNote)
        {
            secretNote = GetComponent<Note>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (!isActive)
        {
            if (secretNote.finished)
            {
                isActive = true;
                SceneManager.LoadScene(6);
            }
        }
	}
}
