using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatform : MonoBehaviour {

    public float lowHeight;
    public float highHeight;
    public float speed = 10f;
    protected bool isRising;

    protected Vector3 originalPos;
    protected bool originalIsRising;

	// Use this for initialization
	void Start () {
        originalPos = transform.position;
        originalIsRising = isRising;
	}

    public void OnReset()
    {
        transform.position = originalPos;
        isRising = originalIsRising;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 tempPos = transform.position;
        if (isRising)
        {
            tempPos.y += speed * Time.deltaTime;
        }
        else
        {
            tempPos.y -= speed * Time.deltaTime;
        }

        tempPos.y = Mathf.Min(highHeight, Mathf.Max(lowHeight, tempPos.y));

        transform.position = tempPos;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isRising = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isRising = false;
        }
    }
}
