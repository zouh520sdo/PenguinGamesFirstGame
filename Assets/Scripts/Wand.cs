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

    private float holdingTime;

    // Use this for initialization
    void Start () {
        tag = "Player";
        holdingTime = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Wandable");
            if (Physics.Raycast(ray, out hit, wandRange, layerMask))
            {
                wandable = hit.collider.gameObject.GetComponent<Wandable>();
                wandable.OnAiming();
            }
            else
            {
                holdingTime = 0;
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
                if (wandable)
                {
                    print("Absorb");
                    holdingTime += (Time.deltaTime);
                }
                absorbFrom(wandable);
            }
            else if (Input.GetButton("Fire2"))
            {
                // Release
                if (wandable)
                {
                    print("Release");
                    holdingTime += (Time.deltaTime);
                }
                releaseTo(wandable);
            }
            else
            {
                holdingTime = 0;
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
            heat = Mathf.Min(maxHeat, heat + w.heatLose(absorbRatio * holdingTime, maxHeat - heat));
        }
    }

    public void releaseTo(Wandable w)
    {
        if (w)
        {
            heat = Mathf.Max(minHeat, heat - w.heatGain(releaseRatio * holdingTime, heat - minHeat));
        }
    }

}