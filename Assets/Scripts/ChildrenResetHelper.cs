using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenResetHelper : MonoBehaviour {

    public List<ObjectIdentifier> childrenToReset;

	// Use this for initialization
	void Start () {
        childrenToReset = new List<ObjectIdentifier>(gameObject.GetComponentsInChildren<ObjectIdentifier>());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnReset()
    {
        foreach (ObjectIdentifier child in childrenToReset)
        {
            child.gameObject.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
        }
    }
}
