using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampWandable : Wandable {

    public Light myLight;
    public bool isOnLight;
    public Renderer lampRenderer;
    public Material unlitMat;
    public Material litMat;
    
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

        if (containingHeat - minHeat > (maxHeat - minHeat) * 0.5f)
        {
            if (lampRenderer.material != litMat)
            {
                lampRenderer.material = litMat;
            }
        }
        else
        {
            if (lampRenderer.material != unlitMat)
            {
                lampRenderer.material = unlitMat;
            }
        }

        if (isOnLight)
        {
            myLight.intensity = lightOnIntensity;
        }
    }
}
