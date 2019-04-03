using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class TutorialTest : MonoBehaviour {


    // Use this for initialization
    void Start () {
        SpriteRenderer sp = this.GetComponentInChildren<SpriteRenderer>();
        sp.color = new Color(1, 1, 1, 0);
        sp.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(Camera.main.transform);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(showText(this.gameObject));
            
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(hideText(this.gameObject));

        }
    }

    IEnumerator showText(GameObject text)
    {

        
        SpriteRenderer sp = text.GetComponentInChildren<SpriteRenderer>();
        sp.enabled = true;
        for (float i = 0; i <= 1.5f; i += Time.deltaTime)
        {
            sp.color = new Color(1, 1, 1, i);
            yield return null;
        }
        
    }

    IEnumerator hideText(GameObject text)
    {
        SpriteRenderer sp = text.GetComponentInChildren<SpriteRenderer>();

        for (float i = 1.5f; i >= 0; i -= Time.deltaTime)
        {
            sp.color = new Color(1, 1, 1, i);
            yield return null;
        }

        sp.color = new Color(1, 1, 1, 0);
        sp.enabled = false;
    }
}
