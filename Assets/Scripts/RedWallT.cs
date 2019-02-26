using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWallT : Triggee {

    public List<Renderer> allRenderers;
    public List<Collider> allColliders;

    public virtual void SetActive(bool active)
    {
        isActive = active;
        foreach (Renderer r in allRenderers)
        {
            r.enabled = !active;
            Color tempColor = r.material.color;
            if (!active)
            {
                tempColor.a = 1f;
                r.material.color = tempColor;
            }
            else
            {
                tempColor.a = 0f;
                r.material.color = tempColor;
            }
        }

        foreach (Collider c in allColliders)
        {
            c.enabled = !active;
        }
    }

    public override void Start()
    {
        base.Start();
        allRenderers = new List<Renderer>(gameObject.GetComponentsInChildren<Renderer>());
        allColliders = new List<Collider>(gameObject.GetComponentsInChildren<Collider>());

        SetActive(isActive);
    }

    public override void Activate()
    {
        base.Activate();
        StartCoroutine(FadeOut());
    }

    public override void Deactivate()
    {
        base.Deactivate();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float alpha = 0f;
        foreach (Renderer r in allRenderers)
        {
            r.enabled = true;
        }
        while (alpha <= 1f)
        {
            foreach (Renderer r in allRenderers)
            {
                Color tempColor = r.material.color;
                tempColor.a = alpha;
                r.material.color = tempColor;
            }

            alpha += Time.deltaTime;
            yield return null;
        }
        SetActive(false);
    }

    IEnumerator FadeOut()
    {
        float alpha = 1f;
        while (alpha >= 0f)
        {
            foreach (Renderer r in allRenderers)
            {
                Color tempColor = r.material.color;
                tempColor.a = alpha;
                r.material.color = tempColor;
            }

            alpha -= Time.deltaTime;
            yield return null;
        }
        SetActive(true);
    }
}
