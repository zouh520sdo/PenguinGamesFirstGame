using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class BackToTileWhenFInished : MonoBehaviour {

    public bool isActive;
    public Note note;
    public int sceneIndex;
    public FirstPersonController fpc;


    // Use this for initialization
    void Start () {
		if (!note)
        {
            note = GetComponent<Note>();
        }

        if (!fpc)
        {
            fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (!isActive)
        {
            if (note.getIsFinished())
            {
                isActive = true;
                fpc.GetMouseLook().SetCursorLock(false);
                SceneManager.LoadScene(sceneIndex);
            }
        }
	}
}
