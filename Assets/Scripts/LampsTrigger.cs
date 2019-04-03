using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampsTrigger : Trigger {

    public List<LampIgnitable> lampsToLight;
    public bool isActive;
    protected bool origianlIsActive;
    public List<LampIgnitable> lampsToUnlit;

    public override void OnReset()
    {
        base.OnReset();
        isActive = origianlIsActive;
        deactivateTriggees();
        foreach (LampIgnitable lamp in lampsToLight)
        {
            lamp.SendMessage("OnReset");
        }
        foreach (LampIgnitable lamp in lampsToUnlit)
        {
            lamp.SendMessage("OnReset");
        }
    }

    public override void Start()
    {
        base.Start();
        origianlIsActive = isActive;
        lampsToUnlit.AddRange(GetComponentsInChildren<LampIgnitable>());
        foreach (LampIgnitable l in lampsToLight)
        {
            lampsToUnlit.Remove(l);
        }
    }

    public override void Update()
    {
        base.Update();

        if (!isActive && lampsToLight.Count != 0)
        {
            foreach (LampIgnitable lamp in lampsToLight)
            {
                isActive = lamp.isOnFire;
                if (!isActive)
                {
                    break;
                }
            }

            if (isActive)
            {
                foreach (LampIgnitable lamp in lampsToUnlit)
                {
                    isActive = !lamp.isOnFire;
                    if (!isActive)
                    {
                        break;
                    }
                }
            }

            if (isActive)
            {
                activateTriggees();
            }
        }
    }

}
