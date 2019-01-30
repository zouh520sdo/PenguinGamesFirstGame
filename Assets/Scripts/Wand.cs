using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour {

    public float heat;
    public float minHeat;
    public float maxHeat;
    public float wandRange;
    public Wandable wandable;
    public float absorbRatio;
    public float releaseRatio;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, wandRange, LayerMask.NameToLayer("Wandable")))
            {
                wandable = hit.collider.gameObject.GetComponent<Wandable>();
                wandable.OnAiming();
            }
            else
            {
                if (wandable != null)
                {
                    wandable.OffAiming();
                    wandable = null;
                }
            }

            // 
            if (Input.GetButton("Fire1"))
            {
                // Absorb
                print("Absorb");
                absorbFrom(wandable);
            }

            if (Input.GetButton("Fire2"))
            {
                // Release
                print("Release");
                releaseTo(wandable);
            }

        }
    }

    /// <summary>
    /// Absorb heat from the given wandable object if it is not null
    /// </summary>
    /// <param name="w">Given wandable object</param>
    public void absorbFrom(Wandable w)
    {
        if (w)
        {
            heat = Mathf.Min(maxHeat, heat + w.heatLose(absorbRatio * Time.deltaTime, maxHeat - heat));
        }
    }

    public void releaseTo(Wandable w)
    {
        if (w)
        {
            heat = Mathf.Max(minHeat, heat - w.heatGain(releaseRatio * Time.deltaTime, heat - minHeat));
        }
    }
}