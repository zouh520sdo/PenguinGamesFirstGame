using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMagic : MonoBehaviour {

    public AudioSource Magic;

    public Image Marker;
    public Sprite Aing;
    public Sprite Ring;

    // Use this for initialization
    void Start()
    {
        Magic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Marker.sprite == Aing || Marker.sprite == Ring)
        {
            //print("YES");
            if (!Magic.isPlaying)
            {
                Magic.Play();
            }
        }
        else
        {
            //Debug.Log("false");
            Magic.Stop();
        }

    }
}
