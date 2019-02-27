using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Pickable {

    public float floatingHeight;

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
