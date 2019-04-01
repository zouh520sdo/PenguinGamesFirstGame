using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour {

    public bool isComplete;
    protected bool originalIsComplete;
    public List<Star> missingStars;

    public void OnReset()
    {
        isComplete = originalIsComplete;
        foreach (Star star in missingStars)
        {
            star.gameObject.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
        }
    }

	// Use this for initialization
	void Start () {
        missingStars.AddRange(GetComponentsInChildren<Star>());

        if (missingStars.Count == 0)
        {
            isComplete = true;
        }
        else
        {
            isComplete = false;
        }

        originalIsComplete = isComplete;

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
        }
	}
}
