using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour {

    public bool isComplete;
    protected bool originalIsComplete;
    public List<Star> missingStars;

    public List<Collider> starColliders;
    public List<ParticleSystem> shinineParticleSystems;

    public void OnReset()
    {
        isComplete = originalIsComplete;
        foreach (Star star in missingStars)
        {
            star.gameObject.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
        }

        SetShiningEffect(isComplete);
    }

	// Use this for initialization
	void Start () {
        missingStars.AddRange(GetComponentsInChildren<Star>());

        starColliders.AddRange(GetComponentsInChildren<Collider>());
        shinineParticleSystems.AddRange(GetComponentsInChildren<ParticleSystem>());

        if (missingStars.Count == 0)
        {
            isComplete = true;
        }
        else
        {
            isComplete = false;
        }

        originalIsComplete = isComplete;
        SetShiningEffect(isComplete);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isComplete)
        {
            foreach (Star star in missingStars)
            {
                isComplete = star.invisibleData.isVisible;
                if (!isComplete)
                {
                    break;
                }
            }

            if (isComplete)
            {
                SetShiningEffect(isComplete);
            }
        }
	}

    public void SetShiningEffect(bool enabled)
    {
        foreach (ParticleSystem p in shinineParticleSystems)
        {
            p.gameObject.SetActive(enabled);
        }
    }
}
