using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(Collider))]
public class InvisiblePlatform : MonoBehaviour {

    public List<GameObject> nextPlatforms;
    public List<GameObject> hidePlatforms;
    public InvisibleData invisibleData;

    public float fadeInDuration;
    public float fadeOutDuration;


    // Date for resetting
    protected InvisibleData originalInvisibleData;
    protected Vector3 originalPos;


    public void OnReset()
    {
        invisibleData = originalInvisibleData;
        originalPos = transform.position;
    }

	// Use this for initialization
	void Start () {
        SetActive(invisibleData.isVisible);
        originalInvisibleData.isVisible = invisibleData.isVisible;
        transform.position = originalPos;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetActive(bool enabled)
    {
        invisibleData.isVisible = enabled;
        GetComponent<Renderer>().enabled = enabled;
        GetComponent<Collider>().enabled = enabled;
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

            foreach (GameObject platform in hidePlatforms)
            {
                StartCoroutine(hidePlatform(platform));
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            
        }
    }

    void OnSave()
    {
        JSONSaveLoad.WriteJSON(name, invisibleData);
    }

    void OnLoad()
    {
        invisibleData = JSONSaveLoad.LoadJSON<InvisibleData>(name);
    }

    IEnumerator showPlatform(GameObject platform)
    {
        float timestamp = Time.time;
        InvisiblePlatform invPlatform = platform.GetComponent<InvisiblePlatform>();

        if (Time.time - timestamp < invPlatform.fadeInDuration)
        {
            yield return null;
        }

        invPlatform.SetActive(true);
    }

    IEnumerator hidePlatform(GameObject platform)
    {
        float timestamp = Time.time;
        InvisiblePlatform invPlatform = platform.GetComponent<InvisiblePlatform>();

        if (Time.time - timestamp < invPlatform.fadeOutDuration)
        {
            yield return null;
        }

        //platform.SetActive(invPlatform.showWhenExit);
        invPlatform.SetActive(false);
    }
}
