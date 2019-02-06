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

    protected float rate;

	// Use this for initialization
	void Start () {
		
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
        }
        else
        {
            ParticleSystem.EmissionModule emission = fire.emission;
            emission.rateOverTime = 0;
            isOnFire = false;
        }

    }

    IEnumerator OnBurnOut ()
    {
        float timestamp = Time.time;
        while (Time.time - timestamp < burnOutDuration)
        {
            yield return null;
        }

        // disable mesh and fire particle
    }

    void OnTriggerEnter(Collider other)
    {
        Wood wood = other.GetComponent<Wood>();
        if (wood)
        {
            lock (fireSources)
            {
                fireSources.Add(wood);
            }
        }

        Ignitable ignitable = other.GetComponent<Ignitable>();
        if (ignitable)
        {
            lock (otherFireSources)
            {
                otherFireSources.Add(ignitable);
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
