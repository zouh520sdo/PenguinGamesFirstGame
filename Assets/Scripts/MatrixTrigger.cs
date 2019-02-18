using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTrigger : Trigger {

    // May need to override activateTriggees and deactivateTriggees
    public Material blueMaterial;
    public Material redMaterial;

    [HideInInspector]
    public Material originalMat;

    public override void Start()
    {
        base.Start();
        originalMat = GetComponent<Renderer>().material;
    }

    public override void OnReset()
    {
        base.OnReset();
        GetComponent<Renderer>().material = originalMat;
    }

    public override void activateTriggees()
    {
        base.activateTriggees();
        GetComponent<Renderer>().material = redMaterial;
    }

    public override void deactivateTriggees()
    {
        base.deactivateTriggees();
        GetComponent<Renderer>().material = blueMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // activate
        if (collision.collider.GetComponent<RedBlock>())
        { 
            activateTriggees();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // deactivate
        if (collision.collider.GetComponent<RedBlock>())
        {
            deactivateTriggees();
        }
    }
}