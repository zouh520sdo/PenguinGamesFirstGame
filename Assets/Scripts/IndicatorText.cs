using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorText : MonoBehaviour {

    public float triggerDistance = 10.0f;
    public float fadeInTime = 0.5f;
    public float stayTime = 2.0f;
    public float fadeOutTime = 0.75f;

    protected Vector3 originalScale;
    protected bool inDistance = false;

    GameObject player;
    Wand wand;

	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;

        player = GameObject.FindGameObjectWithTag("Player");
        wand = player.GetComponent<Wand>();

        StartCoroutine(CheckDistance());
	}

    IEnumerator CheckDistance()
    {
        while (!Input.GetKeyDown(KeyCode.Q))
        {
            transform.localScale = Vector3.zero;
            yield return null;
        }
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float startTime = Time.time;

        while (Time.time - startTime < fadeInTime)
        {
            transform.localScale = originalScale * Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeInTime);
            yield return null;
        }
        transform.localScale = originalScale;

        StartCoroutine(Stay());
    }

    IEnumerator Stay()
    {
        float startTime = Time.time;

        while (Time.time - startTime < stayTime)
        {
            yield return null;
        }

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float startTime = Time.time;
        while (Time.time - startTime < fadeOutTime)
        {
            transform.localScale = originalScale * (1f - Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeOutTime));
            yield return null;
        }

        transform.localScale = Vector3.zero;

        StartCoroutine(CheckDistance());
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
    }
}
