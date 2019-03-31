using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Note))]
public class NoteTrigger : Trigger {

    public bool isActive;
    protected bool originalIsActive;
    protected Note note;

    public override void OnReset()
    {
        base.OnReset();
        isActive = originalIsActive;
        deactivateTriggees();
    }

    public override void Start()
    {
        base.Start();
        note = GetComponent<Note>();
        originalIsActive = isActive;
        if (isActive)
        {
            activateTriggees();
        }
        else
        {
            deactivateTriggees();
        }
    }

    public override void Update()
    {
        base.Update();
        if (!isActive)
        {
            if (note.getIsFinished())
            {
                activateTriggees();
                isActive = true;
            }
        }
    }

}
