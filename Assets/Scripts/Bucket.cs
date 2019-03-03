using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Pickable {

    public float floatingHeight;
    public bool hasWater;

    public GameObject Water;

    protected bool originalHasWater;

    public override void Start()
    {
        base.Start();
        originalHasWater = hasWater;
        SetWaterActive(hasWater);
    }

    public void OnReset()
    {
        hasWater = originalHasWater;
    }

    public void SetWaterActive(bool active)
    {
        hasWater = active;
        Water.SetActive(active);
    }

    public override void Update()
    {
        base.Update();

        if (!isPickedUp)
        {
            Vector3 targetPos = transform.position;
            targetPos.y = floatingHeight;

            targetPos.y += Mathf.Sin(2f * Time.time);

            transform.position = Vector3.Lerp(transform.position, targetPos, 1.5f * Time.deltaTime);
        }
    }
}
