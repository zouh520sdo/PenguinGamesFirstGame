using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggee : MonoBehaviour {

    public bool isActive;

    // Data for resetting
    protected Vector3 originalPos;

    public virtual void OnReset()
    {
        transform.position = originalPos;
    }

    public virtual void Awake()
    {

    }

    // Use this for initialization
    public virtual void Start () {
        originalPos = transform.position;
    }

    // Update is called once per frame
    public virtual void Update () {
        
    }

    public virtual void Activate()
    {
        isActive = true;
    }

    public virtual void Deactivate()
    {
        isActive = false;
    }

}
