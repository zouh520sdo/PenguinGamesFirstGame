using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StarType
{
    Large,
    Small
}

public class Star : Pickable {

    public StarType type;
    public bool isDyingOut;
    public float largeLivingDuration = 10f;
    public float smallLivingDuration = 7f;
    public InvisibleData invisibleData;
    public Renderer myRenderer;
    protected float livingDuration;
    protected float originalLivingDuration;
    protected InvisibleData originalInvisibleData;

    public void  SetActive(InvisibleData enabled)
    {
        myRenderer.enabled = enabled.isVisible;
        GetComponent<Collider>().enabled = enabled.isVisible;
    }

    public override void OnReset()
    {
        base.OnReset();
        livingDuration = originalLivingDuration;
        invisibleData = originalInvisibleData;
        SetActive(invisibleData);
    }

    public override void Start()
    {
        base.Start();
        if (type == StarType.Large)
        {
            livingDuration = largeLivingDuration;
        }
        else
        {
            livingDuration = smallLivingDuration;
        }
        if (!myRenderer)
        {
            myRenderer = GetComponent<Renderer>();
        }
        originalLivingDuration = livingDuration;
        originalInvisibleData = invisibleData;
        SetActive(invisibleData);
    }

    public override void Update()
    {
        base.Update();

        if (isDyingOut && invisibleData.isVisible)
        {
            if (livingDuration <= 0)
            {
                // Set invisible info, and hide
                if (picker)
                {
                    Drop(picker);
                }
                invisibleData.isVisible = false;
                SetActive(invisibleData);
            }
            else
            {
                livingDuration -= Time.deltaTime;
            }
        }
    }

    public override void Pick(Wand p)
    {
        base.Pick(p);
        isDyingOut = true;
    }
}
