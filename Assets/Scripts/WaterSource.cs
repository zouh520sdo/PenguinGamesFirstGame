using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Bucket bucket = other.GetComponent<Bucket>();
        if (bucket)
        {
            bucket.SetWaterActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}