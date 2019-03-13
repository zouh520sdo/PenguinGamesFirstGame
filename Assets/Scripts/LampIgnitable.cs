using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampIgnitable : Ignitable {

    public Light myLight;
    public float groudHeight;

    protected Vector3 targetPos;
    protected bool originalLight;
    protected bool isLightingUp;
    protected float randomDelay;
    protected float randomFrec;
    protected float lightOnIntensity;
    protected float lightOffIntensity;
    protected float targetIntensity;

    public override void Start()
    {
        base.Start();
        //originalLight = myLight.enabled;
        randomDelay = 0.5f * Random.value;
        randomFrec = Random.Range(2f, 4f);
        lightOnIntensity = 2.71f;
        lightOffIntensity = 0f;
        isLightingUp = false;
    }

    public override void OnReset()
    {
        isLightingUp = false;
        containingHeat = originalContainingHeat;
        burnOutDuration = originalBurnOutDuration;
        transform.position = originalPos;
        invisibleData = originalInvisiableData;
        SetActive(invisibleData.isVisible);


        //myLight.enabled = originalLight;
    }

    public override void SetActive(bool enabled)
    {
        invisibleData.isVisible = enabled;
        isOnFire = enabled;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = !enabled;
        }

        if (myLight)
        {
            //myLight.enabled = enabled;
            if (enabled)
            {
                containingHeat = maxHeat;
                myLight.intensity = lightOnIntensity;
            }
            else
            {
                containingHeat = 0f;
                myLight.intensity = lightOffIntensity;
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
            //myLight.enabled = true;
            //isOnFire = true;
            isLightingUp = true;
            targetIntensity = lightOnIntensity;
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
            //myLight.enabled = false;
            targetIntensity = lightOffIntensity;
            isOnFire = false;
        }

        myLight.intensity = Mathf.Lerp(myLight.intensity, targetIntensity, 1.5f * Time.deltaTime);
        if (isLightingUp && lightOnIntensity - myLight.intensity < 0.05f)
        {
            isOnFire = true;
            isLightingUp = false;
            myLight.intensity = lightOnIntensity;
        }


        if (isOnFire)
        {
            myLight.intensity = 2.71f + 0.075f * Mathf.Sin(randomFrec * Mathf.PI * Time.time + randomDelay);
            targetPos = originalPos + new Vector3(0f, 1f * Mathf.Sin(3f * Time.time + 20f * randomDelay), 0f);
        }
        else
        {
            //myLight.enabled = false;
            targetPos = transform.position;
            targetPos.y = groudHeight;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f * Time.deltaTime);
    }
}
