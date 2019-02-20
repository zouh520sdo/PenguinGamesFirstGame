using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Wandable {

    public ParticleSystem fire;
    public bool isOnFire;

    public AudioSource OnFire;

    // Subject to change
    public override void OnAiming()
    {
        base.OnAiming();
        Color targetColor = Color.Lerp(Color.cyan, Color.red, (containingHeat - minHeat) / (maxHeat - minHeat));
        myRenderer.material.color = Color.Lerp(Color.white, targetColor, Mathf.Sin(2 * Mathf.PI * _animingTime));
    }

    public override void OffAiming()
    {
        base.OffAiming();
        myRenderer.material.color = Color.white;
    }

    protected override void OnStart()
    {
        base.OnStart();
        OnFire = GetComponent<AudioSource>();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        float v = volume == 0 ? 1 : volume;
        if (containingHeat / v >= 100)
        {
            ParticleSystem.EmissionModule emission =  fire.emission;
            emission.rateOverTime = 3;
            isOnFire = true;
            if (!OnFire.isPlaying)
            {
                OnFire.Play();
            }
        }
        else
        {
            ParticleSystem.EmissionModule emission = fire.emission;
            emission.rateOverTime = 0;
            isOnFire = false;
            OnFire.Stop();
        }

    }
}
