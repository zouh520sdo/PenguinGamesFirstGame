using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggee : MonoBehaviour {

    public bool isActive;

    public virtual void Awake()
    {

    }

    // Use this for initialization
    public virtual void Start () {
        print(name + " On Start");
	}

    // Update is called once per frame
    public virtual void Update () {
        
    }

    public virtual void Activate()
    {
        isActive = true;
    }

    public virtual void Deactivate()
    {
        isActive = false;
    }

}
