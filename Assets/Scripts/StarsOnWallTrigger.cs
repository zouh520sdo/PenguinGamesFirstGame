using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsOnWallTrigger : Trigger {

    public bool isActive;
    public List<Constellation> constellations;
    protected bool originalIsActive;

    public override void Start()
    {
        base.Start();
        constellations.AddRange(GetComponentsInChildren<Constellation>());
        originalIsActive = isActive;
    }

    public override void OnReset()
    {
        base.OnReset();
        foreach (Constellation c in constellations)
        {
            c.SendMessage("OnReset");
        }
        isActive = originalIsActive;
        deactivateTriggees();
    }

    public override void Update()
    {
        base.Update();
        if (!isActive)
        {
            foreach (Constellation c in constellations)
            {
                isActive = c.isComplete;
                if (!isActive)
                {
                    break;
                }
            }

            if (isActive)
            {
                activateTriggees();
            }
        }
    }
}
