using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    public bool isPickedUp;
    public Wand picker;
    public float distanceRatio;

    protected Rigidbody rigid;
    protected Vector3 targetPosition;

    public virtual void Awake()
    {

    }

	// Use this for initialization
	public virtual void Start () {
        if (gameObject.layer != LayerMask.NameToLayer("Wandable"))
        {
            gameObject.layer = LayerMask.NameToLayer("Pickable");
        }
        isPickedUp = false;
        rigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	public virtual void Update () {
		if (isPickedUp)
        {
            distanceRatio = Mathf.Min(1f, Mathf.Max(0.08f, distanceRatio + 0.5f * Input.GetAxis("Mouse ScrollWheel")));

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            int boundaryLayer = 1 << LayerMask.NameToLayer("Boundary");
            if (Physics.Raycast(ray, out hit, distanceRatio * picker.wandRange, boundaryLayer))
            {
                distanceRatio = hit.distance / picker.wandRange - 0.03f;
            }
            targetPosition = Camera.main.transform.position + distanceRatio * picker.wandRange * Camera.main.transform.forward;
            transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
        }
	}

    // Receive message from ignitable
    void OnInactive ()
    {
        if (picker)
        {
            picker.pickable = null;
            Drop(picker);
        }
    }

    public virtual void Pick(Wand p)
    {
        picker = p;
        p.distanceIndicator.invisibleData.isVisible = true;
        if (rigid)
        {
            rigid.isKinematic = true;
            rigid.useGravity = false;
        }
        isPickedUp = true;

        distanceRatio = Vector3.Distance(transform.position, Camera.main.transform.position) / picker.wandRange;
    }

    public virtual void Drop(Wand p)
    {
        picker = null;
        p.distanceIndicator.invisibleData.isVisible = false;
        if (rigid)
        {
            rigid.isKinematic = false;
            rigid.useGravity = true;
        }
        isPickedUp = false;
    }
}
