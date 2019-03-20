using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buckets : MonoBehaviour {

    public List<Bucket> buckets;

    public void OnReset()
    {
        foreach (Bucket b in buckets)
        {
           // b.enabled = true;
            b.SendMessage("OnReset");
           // b.enabled = false;
        }
    }

	// Use this for initialization
	void Start () {
		if (buckets.Count == 0)
        {
            buckets.AddRange(GetComponentsInChildren<Bucket>());
        }

        foreach (Bucket b in buckets)
        {
            b.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
