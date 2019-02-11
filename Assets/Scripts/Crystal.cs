using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {

    public List<ObjectIdentifier> resettingObjects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetObjects ()
    {
        foreach (ObjectIdentifier ob in resettingObjects)
        {
            ob.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
        }
    }
}