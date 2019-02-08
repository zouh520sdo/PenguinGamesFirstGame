using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public List<Triggee> triggees;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Testing 
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (Triggee t in triggees)
            {
                if (t.isActive)
                {
                    t.Deactivate();
                }
                else
                {
                    t.Activate();
                }
            }
        }

    }
}
