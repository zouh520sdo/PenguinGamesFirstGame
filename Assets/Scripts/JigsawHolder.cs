using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawHolder : MonoBehaviour {

    public JigsawType type;
    public bool isPlaced;

    protected bool originalIsPlaced;

	// Use this for initialization
	void Start () {
        originalIsPlaced = isPlaced;
    }

    void OnReset()
    {
        isPlaced = originalIsPlaced;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
