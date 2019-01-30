using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : Wandable {

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

}
