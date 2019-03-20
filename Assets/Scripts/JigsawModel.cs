using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawModel : Trigger {

    public bool isActive;
    public List<JigsawHolder> holders;

    protected bool originalIsActive;

    public override void OnReset()
    {
        base.OnReset();
        isActive = originalIsActive;
        foreach (JigsawHolder holder in holders)
        {
            holder.SendMessage("OnReset");
        }

        deactivateTriggees();
    }

    public override void Start()
    {
        base.Start();
        originalIsActive = isActive;
    }

    public override void Update()
    {
        base.Update();
        if (!isActive)
        {
            foreach (JigsawHolder holder in holders) {
                if (holder.isPlaced)
                {
                    isActive = true;
                }
                else
                {
                    isActive = false;
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
