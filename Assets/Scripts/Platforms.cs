﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : Triggee {

    public override void OnReset()
    {
        base.OnReset();
        foreach (Transform child in transform)
        {
            child.gameObject.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
        }
    }

    public override void Activate()
    {
        base.Activate();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
