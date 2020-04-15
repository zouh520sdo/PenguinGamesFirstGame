using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : Note {
    public AudioSource memoryAudioSource;

    public override void Start()
    {
        base.Start();
        memoryAudioSource = gameObject.GetComponent<AudioSource>();
    }
}