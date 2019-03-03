using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWater : Wandable {

    public Renderer ice;
    protected Vector3 originalIceScale;
    protected float scaleRatio, targetScaleRatio;
    public Renderer water;

    protected override void OnStart()
    {
        base.OnStart();
        originalIceScale = ice.transform.localScale;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        float v = volume == 0 ? 1 : volume;

        // Change between ice and water
        if (containingHeat / v <= 0f)
        {
            //ice.enabled = true;
            targetScaleRatio = 1f;
        }
        else
        {
            water.enabled = true;
            targetScaleRatio = 0f;
            //ice.enabled = false;
        }

        if (scaleRatio == 1f)
        {
            water.enabled = false;
        }

        if (scaleRatio > targetScaleRatio)
        {
            scaleRatio = Mathf.Max(scaleRatio - Time.deltaTime, targetScaleRatio);
        }

        if (scaleRatio < targetScaleRatio)
        {
            scaleRatio = Mathf.Min(scaleRatio + Time.deltaTime, targetScaleRatio);
        }

        ice.transform.localScale = originalIceScale * scaleRatio;
    }


}