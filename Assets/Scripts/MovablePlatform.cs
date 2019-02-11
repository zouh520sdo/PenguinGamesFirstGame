using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour {

    protected float xPos;
    protected GameObject player;

    protected Vector3 originalPos;

    public void OnReset()
    {
        transform.position = originalPos;
    }

	// Use this for initialization
	void Start () {
        xPos = transform.position.x;

        originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.x = xPos + 4 * Mathf.Sin(0.4f * Mathf.PI * Time.timeSinceLevelLoad);
        transform.position = pos;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
            other.transform.parent = null;
        }
    }
}
