﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BouncingMushroom : MonoBehaviour {

    public float bonusSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Trigger " + other.name);
            FirstPersonController fpc = other.GetComponent<FirstPersonController>();
            fpc.ImpulseJump(bonusSpeed);
        }
    }
}
