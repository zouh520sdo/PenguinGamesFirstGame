using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour {

    public GameObject Btn;
    //Text BtnText;

	// Use this for initialization
	void Start () {
        Btn.GetComponentInChildren<Text>().fontStyle = FontStyle.Italic;
    }
	
	public void isONB()
    {
        Btn.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
        Btn.GetComponentInChildren<Text>().color = Color.white;
    } 

    public void NotOnB()
    {
        Btn.GetComponentInChildren<Text>().fontStyle = FontStyle.Italic;
    }
	
}
