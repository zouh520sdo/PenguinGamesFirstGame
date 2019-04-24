using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportStart : MonoBehaviour {

    private Animator platform;
    public GameObject effect;


	// Use this for initialization
	void Start () {
		platform = this.GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            effect.SetActive(false);
            platform.SetTrigger("end");
            
        }
        
    }


}
