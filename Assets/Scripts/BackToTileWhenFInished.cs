using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTileWhenFInished : MonoBehaviour {

    public bool isActive;
    public Note note;
    public int sceneIndex;

	// Use this for initialization
	void Start () {
		if (!note)
        {
            note = GetComponent<Note>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (!isActive)
        {
            if (note.getIsFinished())
            {
                isActive = true;
                SceneManager.LoadScene(sceneIndex);
            }
        }
	}
}
