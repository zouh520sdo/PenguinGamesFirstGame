using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTriggee : Triggee {

    public override void Activate()
    {
        base.Activate();
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
