using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MedicineType
{
    A,
    B,
    C,
    D
}

public class Medicine : MonoBehaviour {

    public MedicineType type;
    public bool isInGoodCondition;
    public float goodHeatLow;
    public float goodHeatHigh;

    protected MedicineType originalType;
    protected bool originalIsInGood;
    protected Vector3 originalPos;

	// Use this for initialization
	void Start () {
        originalType = type;
        originalIsInGood = isInGoodCondition;
        originalPos = transform.position;
	}

    public void OnReset()
    {
        type = originalType;
        isInGoodCondition = originalIsInGood;
        transform.position = originalPos;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
