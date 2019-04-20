using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampWandable : Wandable {

    public Light myLight;
    public bool isOnLight;
    protected bool originalIsOnLight;
    protected float lightOnIntensity;
    protected float lightOffIntensity;


    protected override void OnStart()
    {
        base.OnStart();
        lightOnIntensity = 2.71f;
        lightOffIntensity = 0f;
        originalIsOnLight = isOnLight;
    }

    public override void OnReset()
    {
        base.OnReset();
        isOnLight = originalIsOnLight;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        myLight.intensity = (lightOnIntensity - lightOffIntensity) * ((containingHeat - minHeat) / (maxHeat - minHeat)) + lightOffIntensity;

        if (containingHeat >= maxHeat)
        {
            isOnLight = true;
        }
        else
        {
            isOnLight = false;
        }

        if (isOnLight)
        {
            myLight.intensity = lightOnIntensity;
        }
    }
}
