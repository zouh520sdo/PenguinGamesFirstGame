using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTrigger : Trigger {

    // May need to override activateTriggees and deactivateTriggees

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