using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MedicineType
{
    A,
    B,
    C
}

public class Medicine : MonoBehaviour {

    public MedicineType type;
    public bool isInGoodCondition;
    public float goodHeatLow;
    public float goodHeatHigh;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
