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
    public bool isOnWall;
    public ParticleSystem shiningEffect;
    public float maxShiningSize = 10f;
    public float minShiningSize = 0f;
    protected float maxLivingDuration;
    protected float livingDuration;
    protected float originalLivingDuration;
    protected InvisibleData originalInvisibleData;
    protected bool originalIsOnWall;

    public void  SetActive(InvisibleData enabled)
    {
        myRenderer.enabled = enabled.isVisible;
        if (!isOnWall)
        {
            GetComponent<Collider>().enabled = enabled.isVisible;
        }
        if (shiningEffect)
        {
            shiningEffect.gameObject.SetActive(enabled.isVisible);
        }
    }

    public override void OnReset()
    {
        base.OnReset();
        livingDuration = originalLivingDuration;
        invisibleData = originalInvisibleData;
        isOnWall = originalIsOnWall;
        SetActive(invisibleData);
        if (shiningEffect)
        {
            ParticleSystem.MainModule main = shiningEffect.main;
            main.startSize = maxShiningSize;
        }
    }

    public override void Start()
    {
        base.Start();
        if (type == StarType.Large)
        {
            livingDuration = largeLivingDuration;
            maxLivingDuration = largeLivingDuration;
        }
        else
        {
            livingDuration = smallLivingDuration;
            maxLivingDuration = smallLivingDuration;
        }
        if (!myRenderer)
        {
            myRenderer = GetComponent<Renderer>();
        }
        if (!shiningEffect)
        {
            shiningEffect = GetComponentInChildren<ParticleSystem>();
        }
        originalLivingDuration = livingDuration;
        originalInvisibleData = invisibleData;
        originalIsOnWall = isOnWall;
        SetActive(invisibleData);

        if (shiningEffect)
        {
            ParticleSystem.MainModule main = shiningEffect.main;
            main.startSize = maxShiningSize;
        }
    }

    public override void Update()
    {
        base.Update();

        if (isDyingOut && invisibleData.isVisible)
        {
            if (livingDuration <= 0)
            {
                DieOut();
            }
            else
            {
                livingDuration = Mathf.Max(0, livingDuration - Time.deltaTime);
                if (shiningEffect)
                {
                    ParticleSystem.MainModule main = shiningEffect.main;
                    main.startSize = (livingDuration / maxLivingDuration) * (maxShiningSize - minShiningSize) + minShiningSize;
                }
            }
        }
    }

    public void DieOut()
    {
        livingDuration = 0;
        // Set invisible info, and hide
        if (picker)
        {
            Drop(picker);
        }
        invisibleData.isVisible = false;
        SetActive(invisibleData);
    }

    public override void Pick(Wand p)
    {
        base.Pick(p);
        isDyingOut = true;
    }

    private void OnTriggerStay(Collider other)
    {
        Star otherStar = other.GetComponent<Star>();
        Pickable otherStarP = other.GetComponent<Pickable>();
        if (isOnWall && !invisibleData.isVisible && otherStar && !otherStar.isOnWall && !otherStarP.isPickedUp)
        {
            otherStar.DieOut();
            invisibleData.isVisible = true;
            SetActive(invisibleData);
        }
    }
}
