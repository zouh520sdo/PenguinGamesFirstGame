﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour {

    //public GameObject Btn;
    //Text BtnText;

	// Use this for initialization
	void Start () {
        GameObject.Find("Start").GetComponentInChildren<Text>().fontStyle = FontStyle.Italic;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
