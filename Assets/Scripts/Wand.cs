using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour {

    public float heat;
    public float wandRange;
    public Wandable wandable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, wandRange, LayerMask.NameToLayer("Wandable")))
        {
            print("Hit wandable.");
            print(hit.collider.name);
            wandable = hit.collider.gameObject.GetComponent<Wandable>();
            wandable.OnAniming();
        }
        else
        {
            if (wandable != null)
            {
                wandable.OffAniming();
                wandable = null;
            }
        }
    }
}