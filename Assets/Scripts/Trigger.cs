using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public List<Triggee> triggees;

    // Data for resetting
    protected Vector3 originalPos;

    public virtual void OnReset()
    {
        transform.position = originalPos;
    }

    public virtual void Awake()
    {

    }

	// Use this for initialization
	public virtual void Start () {
        originalPos = transform.position;
	}

    // Update is called once per frame
    public virtual void Update () {

        // Testing 
        if (Input.GetKeyDown(KeyCode.T))
        {
            activateTriggees();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            deactivateTriggees();
        }
    }

    public virtual void activateTriggees ()
    {
        foreach (Triggee t in triggees)
        {
            t.Activate();
        }
    }

    public virtual void deactivateTriggees ()
    {
        foreach (Triggee t in triggees)
        {
            t.Deactivate();
        }
    }
}
