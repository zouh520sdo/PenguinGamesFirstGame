using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceIndicator : MonoBehaviour {

    public InvisibleData invisibleData;

    protected Light light;
    protected InvisibleData originalIvisibleData;

    public void OnReset()
    {
        invisibleData = originalIvisibleData;
    }

    void OnSave()
    {
        JSONSaveLoad.WriteJSON(name, invisibleData);
    }

    void OnLoad()
    {
        invisibleData = JSONSaveLoad.LoadJSON<InvisibleData>(name);
    }

    // Use this for initialization
    void Start () {
        originalIvisibleData = invisibleData;
        light = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
		if (invisibleData.isVisible)
        {
            light.enabled = true;
        }
        else
        {
            light.enabled = false;
        }
    }
}