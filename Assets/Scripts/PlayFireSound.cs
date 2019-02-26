using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFireSound : MonoBehaviour {

    public GameObject CanFire;
    public AudioSource OnFire;
    private bool isOnfire;

    // Use this for initialization
    void Start () {
        OnFire = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        isOnfire = CanFire.GetComponent<Ignitable>().isOnFire;
        if (isOnfire)
        {
            if (!OnFire.isPlaying)
            {
                OnFire.Play();
            }
        }
        else
        {
            OnFire.Stop();
        }
		
	}
}
