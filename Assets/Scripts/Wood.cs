using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Wandable {

    public ParticleSystem fire;
    public bool isOnFire;

    // Subject to change
    public override void OnAiming()
    {
        base.OnAiming();
        Color targetColor = Color.Lerp(Color.cyan, Color.red, (containingHeat - minHeat) / (maxHeat - minHeat));
        _renderer.material.color = Color.Lerp(Color.white, targetColor, Mathf.Sin(2 * Mathf.PI * _animingTime));
    }

    public override void OffAiming()
    {
        base.OffAiming();
        _renderer.material.color = Color.white;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        print("Wood " + originalPos);

        float v = volume == 0 ? 1 : volume;
        if (containingHeat / v >= 100)
        {
            ParticleSystem.EmissionModule emission =  fire.emission;
            emission.rateOverTime = 3;
            isOnFire = true;
        }
        else
        {
            ParticleSystem.EmissionModule emission = fire.emission;
            emission.rateOverTime = 0;
            isOnFire = false;
        }
    }

}
