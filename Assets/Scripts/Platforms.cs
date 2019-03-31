using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour {

    public void OnReset()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
