using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buckets : Trigger {

    public bool isActive;
    public List<Bucket> buckets;

    public GameObject cagesObject;
    public List<Cage> cages;
    public List<IceWater> waters;

    protected bool originalIsActive;

    public override void OnReset()
    {
        base.OnReset();
        isActive = originalIsActive;
        foreach (Bucket b in buckets)
        {
           // b.enabled = true;
            b.SendMessage("OnReset");
           // b.enabled = false;
        }
    }

    // Use this for initialization
    public override void Start () {
        base.Start();

        originalIsActive = isActive;

        if (buckets.Count == 0)
        {
            buckets.AddRange(GetComponentsInChildren<Bucket>());
        }

        foreach (Bucket b in buckets)
        {
            b.enabled = false;
            waters.Add(b.GetComponentInChildren<IceWater>());
        }

        cages = new List<Cage>(cagesObject.GetComponentsInChildren<Cage>());

        foreach (Cage c in cages)
        {
            if (!c.isActive)
            {
                c.gameObject.SetActive(false);
            }
        }
    }

    public override void Update()
    {
        base.Update();

        if (!isActive)
        {
            for (int i=0; i < waters.Count; i++)
            {
                if (waters[i].ice.enabled == cages[i].isActive)
                {
                    isActive = true;
                }
                else
                {
                    isActive = false;
                    break;
                }
            }

            if (isActive)
            {
                activateTriggees();
            }
        }
    }
}
