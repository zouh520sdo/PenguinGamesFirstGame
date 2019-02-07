using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour {

    public float heat;
    public float minHeat;
    public float maxHeat;
    public float wandRange;
    public Wandable wandable;
    public Pickable pickable;
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

        if (pickable)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
            {
                pickable.Drop(this);
                pickable = null;
            }
        }
        else
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            int wandableLayer = 1 << LayerMask.NameToLayer("Wandable");
            int pickableLayer = 1 << LayerMask.NameToLayer("Pickable");
            int layerMask = wandableLayer | pickableLayer;
            if (Physics.Raycast(ray, out hit, wandRange, layerMask))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    wandable = hit.collider.gameObject.GetComponent<Wandable>();
                    if (wandable)
                    {
                        wandable.OnAiming();
                        // 
                        if (Input.GetButton("Fire1"))
                        {
                            // Absorb
                            if (wandable)
                            {
                                holdingTime += (Time.deltaTime);
                            }
                            absorbFrom(wandable);
                        }
                        else if (Input.GetButton("Fire2"))
                        {
                            // Release
                            if (wandable)
                            {
                                holdingTime += (Time.deltaTime);
                            }
                            releaseTo(wandable);
                        }
                        else
                        {
                            holdingTime = 0;
                        }
                    }
                    else
                    {
                        holdingTime = 0;
                    }
                }
                else
                {
                    if (!pickable)
                    {
                        if (Input.GetButtonDown("Fire1"))
                        {
                            pickable = hit.collider.GetComponent<Pickable>();
                            if (pickable)
                            {
                                pickable.Pick(this);
                            }
                        }
                    }
                }
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