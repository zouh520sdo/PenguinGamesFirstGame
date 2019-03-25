using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : Trigger {

    private void OnTriggerEnter(Collider other)
    {
        activateTriggees();
    }
}
