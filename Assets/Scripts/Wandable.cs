﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandable : MonoBehaviour {
    public GameObject indicatorObjPrefab;

    public float maxHeat;
    public float minHeat;
    public float containingHeat;
    public float volume;
    public Renderer myRenderer;
    protected float _animingTime;

    // Date for resetting
    protected float originalContainingHeat;
    protected Vector3 originalPos;

	// Use this for initialization
	void Start () {
        OnStart();
    }
	
	// Update is called once per frame
	void Update () {
        OnUpdate();
    }

    void Awake()
    {
        OnAwake();
    }

    public virtual void OnReset()
    {
        containingHeat = originalContainingHeat;
        transform.position = originalPos;
        _animingTime = 0;
    }

    protected virtual void OnStart()
    {
        if (indicatorObjPrefab)
        {
            Instantiate(indicatorObjPrefab, transform);
        }

        if (!myRenderer)
        {
            myRenderer = GetComponent<Renderer>();
        }
        gameObject.layer = LayerMask.NameToLayer("Wandable");
        tag = "Wandable";
        _animingTime = 0;
        originalContainingHeat = containingHeat;
        originalPos = transform.position;
    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnAwake()
    {

    }

    public virtual void OnAiming ()
    {
        _animingTime += Time.deltaTime;
    }

    public virtual void OffAiming ()
    {
        _animingTime = 0;
    }

    /// <summary>
    /// Attemp to decrease heat in this object
    /// </summary>
    /// <param name="amount">amount to decrease</param>
    /// <param name="wandRemaining">amount remaining space in wand to be empty</param>
    /// <returns>The actual decreasing amount </returns>
    public float heatLose(float amount, float wandRemaining)
    {
        float temp = Mathf.Max(minHeat, containingHeat - amount);
        float diff = Mathf.Min(containingHeat - temp, wandRemaining);
        containingHeat -= diff;
        return diff;
    }

    /// <summary>
    /// Attemp to increase heat in this object
    /// </summary>
    /// <param name="amount">amount to increase</param>
    /// <param name="wandRemaining">amount remaining space in wand to be full</param>
    /// <returns>The actual increasing amount</returns>
    public float heatGain(float amount, float wandRemaining)
    {
        float temp = Mathf.Min(maxHeat, containingHeat + amount);
        float diff = Mathf.Min(temp - containingHeat, wandRemaining);
        containingHeat += diff;
        return diff;
    }
}
