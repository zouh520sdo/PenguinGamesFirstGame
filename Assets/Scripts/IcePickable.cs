using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePickable : Pickable {

    public Bucket bucket;
    public GameObject icePrefab;

    public override void Start()
    {
        base.Start();
        
        if (!icePrefab)
        {
            icePrefab = transform.GetChild(0).gameObject;
            icePrefab.GetComponent<Collider>().enabled = false;
            icePrefab.GetComponent<Pickable>().enabled = false;
            icePrefab.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public override void Update()
    {
        base.Update();
        if (GetComponent<IceWater>().scaleRatio == 1f)
        {
            isPickable = true;
        }
        else
        {
            isPickable = false;
        }
    }

    public override void Pick(Wand p)
    {
        GameObject newIce = Instantiate(icePrefab, gameObject.transform);
        newIce.transform.SetParent(null);
        bucket.ResetOnIcePicked();
        //newIce.transform.position = transform.position;
        //newIce.transform.rotation = transform.rotation;
        //newIce.transform.localScale = transform.lossyScale;
        Pickable newIcePickable = newIce.GetComponent<Pickable>();
        newIcePickable.picker = p;
        p.distanceIndicator.invisibleData.isVisible = true;
        if (newIcePickable.rigid)
        {
            newIcePickable.rigid.isKinematic = true;
            newIcePickable.rigid.useGravity = false;
        }
        newIcePickable.isPickedUp = true;
        newIcePickable.distanceRatio = Vector3.Distance(transform.position, Camera.main.transform.position) / newIcePickable.picker.wandRange;
        newIcePickable.GetComponent<Pickable>().enabled = true;
        newIcePickable.GetComponent<Collider>().enabled = true;

        p.pickable = newIcePickable;
    }

    public override void Drop(Wand p)
    {
        base.Drop(p);
    }

}
