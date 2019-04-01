using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingDoorTriggee : Triggee {

    public override void Activate()
    {
        base.Activate();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
