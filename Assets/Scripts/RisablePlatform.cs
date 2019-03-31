using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisablePlatform : Wandable {

    public float lowHeight;
    public float highHeight;

    protected override void OnStart()
    {
        base.OnStart();

        float height = transform.position.y;
        containingHeat = (height - lowHeight) / (highHeight - lowHeight) * (maxHeat - minHeat) + minHeat;
        containingHeat = Mathf.Min(maxHeat, Mathf.Max(minHeat, containingHeat));
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        Vector3 tempPos = transform.position;
        tempPos.y = ((containingHeat - minHeat) / (maxHeat - minHeat)) * (highHeight - lowHeight) + lowHeight;
        transform.position = tempPos;
    }
}
