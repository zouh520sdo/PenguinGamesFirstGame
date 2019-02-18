using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchHolder : MonoBehaviour {

    public TorchPickable torchOnHolder;
    public Vector3 holderPos;
    public Vector3 holderRot;
    protected Quaternion holderRotQua;

    protected TorchPickable originalTorchOnHolder;

    public void OnReset()
    {
        torchOnHolder = originalTorchOnHolder;
    }

    public void load(TorchPickable w)
    {
        w.transform.position = holderPos;
        w.transform.rotation = holderRotQua;
        w.GetComponent<Rigidbody>().isKinematic = true;
        torchOnHolder = w;
    }

    public void unload(TorchPickable w)
    {
        torchOnHolder = null;
        w.GetComponent<Rigidbody>().isKinematic = true;
    }

	// Use this for initialization
	void Start () {
        originalTorchOnHolder = torchOnHolder;
        holderRotQua = Quaternion.Euler(holderRot);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
