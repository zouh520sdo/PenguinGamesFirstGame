using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : Wandable {


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

}