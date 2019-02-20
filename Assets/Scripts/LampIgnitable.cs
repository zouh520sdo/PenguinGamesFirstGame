using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampIgnitable : Ignitable {

    public Light myLight;

    protected bool originalLight;
    protected float randomDelay;
    protected float randomFrec;

    public override void Start()
    {
        base.Start();
        originalLight = myLight.enabled;
        randomDelay = 0.5f * Random.value;
        randomFrec = Random.Range(2f, 4f);
    }

    public override void OnReset()
    {
        containingHeat = originalContainingHeat;
        burnOutDuration = originalBurnOutDuration;
        transform.position = originalPos;
        invisibleData = originalInvisiableData;
        SetActive(invisibleData.isVisible);
        myLight.enabled = originalLight;
    }

    public override void SetActive(bool enabled)
    {
        invisibleData.isVisible = enabled;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = !enabled;
        }

        if (myLight)
        {
            myLight.enabled = enabled;
            if (enabled)
            {
                containingHeat = maxHeat;
            }
            else
            {
                containingHeat = 0f;
            }
        }

        // Send message to any compnoments that need to be notified with the activition of this ignitable object
        if (enabled)
        {
            gameObject.SendMessage("OnActive", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            gameObject.SendMessage("OnInactive", SendMessageOptions.DontRequireReceiver);
        }
    }

    // Update is called once per frame
    public override void Update () {

        float v = volume == 0 ? 1 : volume;
        if (!isOnFire)
        {
            // Calculate rate based on fire sources
            rate = 0f;
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
            myLight.enabled = true;
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
            myLight.enabled = false;
            isOnFire = false;
        }

        myLight.intensity = 2.71f + 0.075f * Mathf.Sin(randomFrec*Mathf.PI*Time.time + randomDelay);

    }
}
