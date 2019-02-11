using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlock : MonoBehaviour {

    // Data for resetting
    protected Vector3 originalPos;

    public virtual void OnReset()
    {
        transform.position = originalPos;
    }

    void Start()
    {
        originalPos = transform.position;
    }

}
