using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignitable : MonoBehaviour {

    public float containingHeat;
    public float maxHeat;
    public float minHeat;
    public float volume;

    public bool isOnFire;
    public ParticleSystem fire;
    public bool canBurnOut;
    public float burnOutDuration;

    public List<Wood> fireSources;
    public List<Ignitable> otherFireSources;

    public InvisibleData invisibleData;

    protected float rate;

    // Use this for initialization
    void Start () {
        SetActive(invisibleData.isVisible);
    }

    // Update is called once per frame
    void Update () {

        float v = volume == 0 ? 1 : volume;
        if (!isOnFire)
        {
            // Calculate rate based on fire sources
            rate = -10;
            lock (fireSources)
            {
                foreach (Wood wood in fireSources)
                {
                    if (wood.isOnFire)
                    {
                        rate += 35;
                    }
                }
            }
            lock (otherFireSources)
            {
                foreach (Ignitable otherFire in otherFireSources)
                {
                    if (otherFire.isOnFire)
                    {
                        rate += 35;
                    }
                }
            }

            containingHeat = Mathf.Max(minHeat, Mathf.Min(maxHeat, containingHeat + rate / v * Time.deltaTime));
        }

        if (containingHeat >= maxHeat)
        {
            ParticleSystem.EmissionModule emission = fire.emission;
            emission.rateOverTime = 3;
            isOnFire = true;
            if (canBurnOut)
            {
                if (burnOutDuration > 0)
                {
                    burnOutDuration -= Time.deltaTime;
                }
                else
                {
                    // disable mesh and fire particle
                    SetActive(false);
                }
            }
        }
        else
        {
            ParticleSystem.EmissionModule emission = fire.emission;
            emission.rateOverTime = 0;
            isOnFire = false;
        }

    }

    public void SetActive(bool enabled)
    {
        invisibleData.isVisible = enabled;
        GetComponent<Renderer>().enabled = enabled;
        GetComponent<Collider>().enabled = enabled;
        GetComponent<Rigidbody>().isKinematic = !enabled;
        fire.gameObject.SetActive(enabled);
    }

    void OnSave()
    {
        JSONSaveLoad.WriteJSON(name, invisibleData);
    }

    void OnLoad()
    {
        invisibleData = JSONSaveLoad.LoadJSON<InvisibleData>(name);
    }

    void OnTriggerEnter(Collider other)
    {
        Wood wood = other.GetComponent<Wood>();
        if (wood)
        {
            lock (fireSources)
            {
                if (!fireSources.Contains(wood))
                {
                    fireSources.Add(wood);
                }
            }
        }

        Ignitable ignitable = other.GetComponent<Ignitable>();
        if (ignitable)
        {
            lock (otherFireSources)
            {
                if (!otherFireSources.Contains(ignitable))
                {
                    otherFireSources.Add(ignitable);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Wood wood = other.GetComponent<Wood>();
        if (wood)
        {
            lock (fireSources)
            {
                fireSources.Remove(wood);
            }
        }

        Ignitable ignitable = other.GetComponent<Ignitable>();
        if (ignitable)
        {
            lock (otherFireSources)
            {
                otherFireSources.Remove(ignitable);
            }
        }
    }
}
