using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    public bool isPickedUp;
    public Wand picker;
    public float distanceRatio;

    protected Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        if (gameObject.layer != LayerMask.NameToLayer("Wandable"))
        {
            gameObject.layer = LayerMask.NameToLayer("Pickable");
        }
        isPickedUp = false;
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		if (isPickedUp)
        {
            distanceRatio = Mathf.Min(1, Mathf.Max(0, distanceRatio + Input.GetAxis("Mouse ScrollWheel")));
            transform.position = Camera.main.transform.position + distanceRatio * picker.wandRange * Camera.main.transform.forward;
        }
	}

    public void Pick(Wand p)
    {
        picker = p;
        if (rigidbody) rigidbody.isKinematic = true;
        isPickedUp = true;

        distanceRatio = Vector3.Distance(transform.position, Camera.main.transform.position) / picker.wandRange;
    }

    public void Drop(Wand p)
    {
        picker = null;
        if (rigidbody) rigidbody.isKinematic = false;
        isPickedUp = false;
    }
}
