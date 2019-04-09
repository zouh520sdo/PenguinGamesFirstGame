using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingDoorTriggee : Triggee {

    public override void Activate()
    {
        base.Activate();
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = true;
        }
        foreach (Collider childCollider in GetComponentsInChildren<Collider>())
        {
            childCollider.enabled = true;
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = false;
        }
        foreach (Collider childCollider in GetComponentsInChildren<Collider>())
        {
            childCollider.enabled = false;
        }
    }
}
