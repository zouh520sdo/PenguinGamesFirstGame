using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFenceT : Triggee {

    public Vector3 topPos;
    public Vector3 botPos;

    protected Vector3 targetPos;

    public override void Activate()
    {
        base.Activate();
        // Goes down 
        targetPos = botPos;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        // Goes up
        targetPos = topPos;
    }

    public override void Start()
    {
        base.Start();
        // Inactive when start
        Deactivate();
    }

    public override void Update()
    {
        base.Update();

        if (Vector3.Distance(targetPos, transform.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
        }
        else
        {
            transform.position = targetPos;
        }
    }
}
