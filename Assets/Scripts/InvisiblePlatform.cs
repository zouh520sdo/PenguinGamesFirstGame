using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class InvisiblePlatform : MonoBehaviour {

    public List<GameObject> nextPlatforms;
    public bool showWhenStart;
    public bool showWhenExit;

    public float fadeInDuration;
    public float fadeOutDuration;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(showWhenStart);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        print("Colliding " + collider.tag);
        if (collider.tag == "Player")
        {
            foreach (GameObject platform in nextPlatforms)
            {
                StartCoroutine(showPlatform(platform));
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(hidePlatform(gameObject));
        }
    }

    IEnumerator showPlatform(GameObject platform)
    {
        float timestamp = Time.time;
        InvisiblePlatform invPlatform = platform.GetComponent<InvisiblePlatform>();

        if (Time.time - timestamp < invPlatform.fadeInDuration)
        {
            yield return null;
        }

        platform.SetActive(true);
    }

    IEnumerator hidePlatform(GameObject platform)
    {
        float timestamp = Time.time;
        InvisiblePlatform invPlatform = platform.GetComponent<InvisiblePlatform>();

        if (Time.time - timestamp < invPlatform.fadeOutDuration)
        {
            yield return null;
        }

        platform.SetActive(invPlatform.showWhenExit);
    }
}
